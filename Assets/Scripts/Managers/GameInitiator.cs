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


    IEnumerator Start()
    {
        yield return StartCoroutine(BindearDatos());
        yield return StartCoroutine(ColocarRespawnPoint());
        yield return StartCoroutine(ColocarPersonaje());
        yield return StartCoroutine(ColocarCamara());
        yield return StartCoroutine(ActivarActionMaps());
        yield return StartCoroutine(RenaudarElTiempo());
    }

    private IEnumerator BindearDatos()
    {
        NivelDataBase data = (NivelDataBase)gameData;
        respawnScript = character.GetComponent<Respawn>();
        respawnScript.respawnPoint = data.respawnPoint;
        cameraX = data.camaraPosicionX;
        cameraY = data.camaraPosicionY;
        cameraSize = data.camaraZoom;
        yield return null;
    }

    private IEnumerator ColocarRespawnPoint()
    {
        respawnScript.transform.position = new Vector3(respawnScript.respawnPoint.x, respawnScript.respawnPoint.y, respawnScript.transform.position.z);
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
        Camera.main.transform.position = new Vector3(cameraX, cameraY, Camera.main.transform.position.z);
        Camera.main.orthographicSize = cameraSize;

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
