using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public TextAsset csvFile;
    public float zoomCamara;
    public Vector3 camPos;
    
    public void TriggerDialogue()
    {
        DialogueManager.instance.StartDialogue(csvFile, zoomCamara, camPos.x, camPos.y);
    }
}
