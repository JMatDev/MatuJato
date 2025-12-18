using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.UI;
using Unity.Cinemachine;

public class DialogueManager : MonoBehaviour
{
    public CinemachineCamera cinemachineCamera;
    public Canvas canvas;
    public InputActionReference move;
    public InputActionReference interact;
    public GameObject prefabDialogueBox;
    public GameObject prefabBlackBackground;
    public AnimationCurve curvaZoom;
    public AnimationCurve curvaBlackBackground;
    public AnimationCurve curvaDesaparicion;
    public float duracionZoom;
    public float duracionDesaparicionTextos;


    private GameObject InstanceDialogueBox;
    private GameObject InstanceBlackBackground; 
    private float startZoom;
    private Vector3 startPos;
    private GameObject personaje;



    //singleton pattern
    public static DialogueManager instance;
    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void StartDialogue(TextAsset csvFile, float zoomCamara, float camPosX, float camPosY)
    {
        StartCoroutine(DialogueCoroutine(csvFile, zoomCamara, camPosX, camPosY));
    }

    private IEnumerator DialogueCoroutine(TextAsset csvFile, float zoomCamara, float camPosX, float camPosY)
    {
        startZoom = cinemachineCamera.Lens.OrthographicSize;
        startPos = cinemachineCamera.transform.position;
        personaje = cinemachineCamera.Follow.gameObject; 

        yield return StartCoroutine(InstanciarPrefabs());
        yield return StartCoroutine(PausarYzoom(zoomCamara, camPosX, camPosY, 260));

        yield return LectorGuion.instance.LeerGuion(csvFile, InstanceDialogueBox, interact);

        yield return StartCoroutine(DestruirPrefabs());
        yield return StartCoroutine(ReanudarYzoom(startZoom, startPos, 1660));
        yield return null;
    }
    
    private IEnumerator InstanciarPrefabs()
    {
        InstanceBlackBackground = Instantiate(prefabBlackBackground, canvas.transform);
        InstanceDialogueBox = Instantiate(prefabDialogueBox, canvas.transform);
        yield return null;
    }

    private IEnumerator PausarYzoom(float zoomCamara, float camPosX, float camPosY, float posBlackX)
    {
        move.action.Disable();
        interact.action.Enable();
        cinemachineCamera.Follow = null;
        yield return ZoomYfondoNegro(zoomCamara, camPosX, camPosY, posBlackX);
        yield return null;
    }

    private IEnumerator ZoomYfondoNegro(float zoomCamara, float camPosX, float camPosY, float posBlackX)
    {
        float prevZoom = cinemachineCamera.Lens.OrthographicSize;
        Vector3 prevPosCam = cinemachineCamera.transform.position;
        Vector3 prevPosBlack = InstanceBlackBackground.transform.localPosition;

        float tiempoTranscurrido = 0f;

        while (tiempoTranscurrido < duracionZoom)
        {
            float t = Mathf.Clamp01(tiempoTranscurrido / duracionZoom);
            float curveZoom = curvaZoom.Evaluate(t);
            float curveBlack = curvaBlackBackground.Evaluate(t);

            cinemachineCamera.Lens.OrthographicSize = Mathf.LerpUnclamped(prevZoom, zoomCamara, curveZoom);
            cinemachineCamera.transform.position = Vector3.LerpUnclamped(prevPosCam, new Vector3(camPosX, camPosY, prevPosCam.z), curveZoom);
            InstanceBlackBackground.transform.localPosition = Vector3.LerpUnclamped(prevPosBlack, new Vector3(posBlackX, 0, 0), curveBlack);

            if (interact.action.triggered) break; //interrumpir animacion

            tiempoTranscurrido += Time.deltaTime;
            yield return null;
        }

        cinemachineCamera.Lens.OrthographicSize = zoomCamara;
        cinemachineCamera.transform.position = new Vector3(camPosX, camPosY, prevPosCam.z);
        InstanceBlackBackground.transform.localPosition = new Vector3(posBlackX, 0, 0);
    }

    private IEnumerator ReanudarYzoom(float startZoom, Vector3 startPos, float posBlackX)
    {
        move.action.Enable();
        interact.action.Disable();
        yield return StartCoroutine(ZoomYfondoNegro(startZoom, startPos.x, startPos.y, posBlackX));
        cinemachineCamera.Follow = personaje.transform;
        Destroy(InstanceBlackBackground);
        yield return null;
    }

    private IEnumerator DestruirPrefabs()
    {
        CanvasGroup canvasGroup = InstanceDialogueBox.GetComponent<CanvasGroup>();
        float tiempo = 0f;

        while (tiempo < duracionDesaparicionTextos)
        {
            float t = Mathf.Clamp01(tiempo / duracionDesaparicionTextos);
            float curvaD = curvaDesaparicion.Evaluate(t);

            canvasGroup.alpha = Mathf.LerpUnclamped(1, 0, curvaD);

            if (interact.action.triggered) break;

            tiempo += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0;
        Destroy(InstanceDialogueBox);
        yield return null;
    }
}
