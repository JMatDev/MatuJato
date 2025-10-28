using UnityEngine;

public class scriptpruebadm : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public TextAsset csvFile;
    public float zoomCamara;
    public float camPosX;
    public float camPosY;

    void OnTriggerEnter2D(Collider2D collision)
    {
        dialogueManager.StartDialogue(csvFile, zoomCamara, camPosX, camPosY);
    }
}
