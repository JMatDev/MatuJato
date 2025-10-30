using UnityEngine;
using System.Collections;

public class GameInitiator : MonoBehaviour
{
    public GameObject respawnPoint;
    public GameObject character;
    public float cameraX, cameraY, cameraSize;
    public float respawnPointX, respawnPointY;
    IEnumerator Start()
    {
        yield return StartCoroutine(ColocarRespawnPoint());
        yield return StartCoroutine(ColocarPersonaje());
        yield return StartCoroutine(ColocarCamara());
        yield return StartCoroutine(RenaudarElTiempo());
    }

    private IEnumerator ColocarRespawnPoint()
    {
        respawnPoint.transform.position = new Vector3(respawnPointX, respawnPointY, respawnPoint.transform.position.z);
        yield return null; 
    }

    private IEnumerator ColocarPersonaje()
    {
        character.GetComponent<Respawn>().RespawnCharacter();
        character.GetComponent<PlayerController>().FirstAnim();
        yield return null;
    }

    private IEnumerator ColocarCamara()
    {
        Camera.main.transform.position = new Vector3(cameraX, cameraY, Camera.main.transform.position.z);
        Camera.main.orthographicSize = cameraSize;

        yield return null;
    }
    
    private IEnumerator RenaudarElTiempo()
    {
        Time.timeScale = 1f;
        yield return null;
    }
}
