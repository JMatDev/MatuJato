using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public TextAsset csvFile;
    public float zoomCamara;
    public float camPosX;
    public float camPosY;
    
    public void TriggerDialogue()
    {
        DialogueManager.instance.StartDialogue(csvFile, zoomCamara, camPosX, camPosY);
    }
}
