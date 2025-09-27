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
        Debug.Log("Colocar Camara");

        Camera.main.transform.position = new Vector3(-0.53f, 0, -10f);
        Camera.main.orthographicSize = 7f;

        yield return null; 
    }
}
