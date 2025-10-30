using UnityEngine;
using UnityEngine.InputSystem;

public class scriptpruebadm : MonoBehaviour
{
    public TextAsset csvFile;
    public float zoomCamara;
    public float camPosX;
    public float camPosY;

    public InputActionReference pause;

    void OnTriggerEnter2D(Collider2D collision)
    {
        DialogueManager.instance.StartDialogue(csvFile, zoomCamara, camPosX, camPosY);
    }
}
