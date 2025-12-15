using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class GameInitiator : MonoBehaviour
{
    public ScriptableObject gameData;
    public GameObject character;
    public InputActionAsset inputActionAsset;

    private Respawn respawnScript;
    private float cameraX, cameraY, cameraSize;
    [SerializeField] private bool esPrimeraCarga = false;


    IEnumerator Start()
    {
        yield return StartCoroutine(BindearDatos());
        //yield return StartCoroutine(ColocarRespawnPoint());
        yield return StartCoroutine(ColocarPersonaje());
        //yield return StartCoroutine(ColocarCamara());
        yield return StartCoroutine(ActivarActionMaps());
        yield return StartCoroutine(RenaudarElTiempo());
    }

    private IEnumerator BindearDatos()
    {
        // obtener datos del nivel
        NivelDataBase data = (NivelDataBase)gameData;
        if (data.nivelName == "Lobby_Data") esPrimeraCarga = true;

        // obtener datos de la camara
        cameraX = data.camaraPosicionX;
        cameraY = data.camaraPosicionY;
        cameraSize = data.camaraZoom;

        // obtener referencia al script respawn y bindear datos
        respawnScript = character.GetComponent<Respawn>();
        // cambiar el punto de respawn desde el scriptable object
        respawnScript.respawnPoint = data.respawnPoint;

        yield return null;
    }

    
    private IEnumerator ColocarPersonaje()
    {
        Debug.Log("Colocando personaje en el respawn point");
        character.GetComponent<Respawn>().RespawnCharacter();
        character.GetComponent<PlayerController>().FirstAnim(); // que el personaje mire al frente
        yield return null;
    }

    /*
    private IEnumerator ColocarCamara()
    {   
        Camera.main.transform.position = new Vector3(cameraX, cameraY, Camera.main.transform.position.z);
        Camera.main.orthographicSize = cameraSize;
        
        yield return null;
    }
    */
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
