using UnityEngine;
using System.Collections;

public class GameInitiator : MonoBehaviour
{
    public GameObject character;
    IEnumerator Start()
    {
        yield return StartCoroutine(ColocarPersonaje());
        yield return StartCoroutine(ColocarCamara());
    }

    private IEnumerator ColocarPersonaje()
    {
        character.GetComponent<Respawn>().RespawnCharacter();
        yield return null;
    }

    private IEnumerator ColocarCamara()
    {
        Camera.main.transform.position = new Vector3(0.1f, -2f, Camera.main.transform.position.z);
        Camera.main.orthographicSize = 10f;

        yield return null; 
    }
}
