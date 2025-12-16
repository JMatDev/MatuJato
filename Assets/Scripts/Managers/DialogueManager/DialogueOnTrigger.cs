using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueOnTrigger : MonoBehaviour
{
    public TextAsset csvFile;
    public float zoomCamara;
    public float camPosX;
    public float camPosY;
    public bool primeraAparicion = false;

    public InputActionReference pause;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!primeraAparicion)
        {
            DialogueManager.instance.StartDialogue(csvFile, zoomCamara, camPosX, camPosY);
            primeraAparicion = true;
        }
    }
}