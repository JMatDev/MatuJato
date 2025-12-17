using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class GameInitiator : MonoBehaviour
{
    public ScriptableObject gameData;
    public GameObject character;
    public InputActionAsset inputActionAsset;
    public CinemachineCamera cameraBrain;


    [SerializeField] private bool esPrimeraCarga = false;
    private Respawn respawnScript;
    private NivelDataBase nivelData;
    private GameObject confinerInst = null;
    


    //singleton pattern
    public static GameInitiator instance;
    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public IEnumerator Start()
    {
        yield return StartCoroutine(FadeIn());
        yield return StartCoroutine(BindearDatos());
        yield return StartCoroutine(ColocarCamaraYpersonaje());
        yield return StartCoroutine(ActivarActionMaps());
        yield return StartCoroutine(RenaudarElTiempo());
        yield return StartCoroutine(FadeOut());
    }

    public IEnumerator FadeIn()
    {
        Debug.Log("FadeIn");
        yield return null;
    }

    private IEnumerator BindearDatos()
    {
        // obtener datos del nivel
        nivelData = (NivelDataBase)gameData;
        if (nivelData.nivelName == "Lobby_Data") esPrimeraCarga = true;
        // obtener referencia al script respawn y bindear datos
        respawnScript = character.GetComponentInChildren<Respawn>();
        // cambiar el punto de respawn desde el scriptable object
        respawnScript.respawnPoint = nivelData.respawnPoint;

        yield return null;
    }

    private IEnumerator ColocarCamaraYpersonaje()
    {
        var confiner = cameraBrain.GetComponent<CinemachineConfiner2D>();
        var composer = cameraBrain.GetComponent<CinemachinePositionComposer>();
        var vcamBase = cameraBrain.GetComponent<CinemachineVirtualCameraBase>(); 

        // Backup
        float slowBuff = confiner.SlowingDistance;
        float dampingBuff = confiner.Damping;
        var composerDampBuff = composer.Damping;

        // Apagar suavizados
        confiner.SlowingDistance = 0f;
        confiner.Damping = 0f;
        composer.Damping = Vector3.zero;

        // Cambiar confiner
        if (confinerInst != null) Destroy(confinerInst);
        confinerInst = Instantiate(nivelData.confiner);

        var bounds = confinerInst.GetComponentInChildren<Collider2D>();
        confiner.BoundingShape2D = bounds;
        confiner.InvalidateBoundingShapeCache();

        cameraBrain.Lens.OrthographicSize = nivelData.camaraZoom;
        composer.Composition.ScreenPosition = nivelData.screenPositionComposer;

        // Teleport del personaje
        Vector3 oldPos = character.transform.position;

        character.transform.localScale = nivelData.escalaPersonaje;
        respawnScript.RespawnCharacter();
        character.GetComponent<PlayerController>().FirstAnim();

        Vector3 delta = character.transform.position - oldPos;

        if (vcamBase != null) vcamBase.OnTargetObjectWarped(character.transform, delta);
        else cameraBrain.PreviousStateIsValid = false; // si tu clase lo tiene
        yield return null;

        // Restaurar
        confiner.SlowingDistance = slowBuff;
        confiner.Damping = dampingBuff;
        composer.Damping = composerDampBuff;
    }

    private IEnumerator ActivarActionMaps()
    {
        var gameplayMap = inputActionAsset.FindActionMap("Gameplay");
        
        foreach (var action in gameplayMap.actions)
        {
            //if action es dialogue no activar
            if (action.name != "Dialogue/Accept") action.Enable();  
        }

        yield return null;
    }
    
    private IEnumerator RenaudarElTiempo()
    {
        Time.timeScale = 1f;
        yield return null;
    }

    public IEnumerator FadeOut()
    {
        Debug.Log("FadeOut");
        yield return null;
    }
}
