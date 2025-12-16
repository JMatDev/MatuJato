using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using Unity.Cinemachine;
using System.Data.Common;

public class GameInitiator : MonoBehaviour
{
    public ScriptableObject gameData;
    public GameObject character;
    public InputActionAsset inputActionAsset;
    public CinemachineCamera cameraBrain;

    private Respawn respawnScript;
    private float cameraX, cameraY, cameraSize;
    NivelDataBase nivelData;
    [SerializeField] private bool esPrimeraCarga = false;


    IEnumerator Start()
    {
        yield return StartCoroutine(BindearDatos());
        yield return StartCoroutine(ColocarPersonaje());
        yield return StartCoroutine(ColocarCamara());
        yield return StartCoroutine(ActivarActionMaps());
        yield return StartCoroutine(RenaudarElTiempo());
    }

    private IEnumerator BindearDatos()
    {
        // obtener datos del nivel
        nivelData = (NivelDataBase)gameData;
        if (nivelData.nivelName == "Lobby_Data") esPrimeraCarga = true;

        // obtener datos de la camara
        cameraX = nivelData.camaraPosicionX;
        cameraY = nivelData.camaraPosicionY;
        cameraSize = nivelData.camaraZoom;

        // obtener referencia al script respawn y bindear datos
        respawnScript = character.GetComponent<Respawn>();
        // cambiar el punto de respawn desde el scriptable object
        respawnScript.respawnPoint = nivelData.respawnPoint;

        yield return null;
    }

    
    private IEnumerator ColocarPersonaje()
    {
        character.GetComponent<Respawn>().RespawnCharacter();
        character.GetComponent<PlayerController>().FirstAnim(); // que el personaje mire al frente
        yield return null;
    }

    
    private IEnumerator ColocarCamara()
    {
        GameObject confinerInst  = Instantiate(nivelData.confiner);
        Collider2D bounds = confinerInst.GetComponentInChildren<Collider2D>();
        //asignar el confiner a la camara
        cameraBrain.GetComponent<CinemachineConfiner2D>().BoundingShape2D = bounds;
        cameraBrain.GetComponent<CinemachineConfiner2D>().InvalidateBoundingShapeCache();
        cameraBrain.Lens.OrthographicSize = nivelData.camaraZoom;
        yield return null;
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
}
