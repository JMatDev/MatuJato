using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

    [ContextMenu("Trigger Dialogue")]
    public void TriggerDialogue() {
        DialogueManager.Instance.StartDialogue(100, 104);
    }

}
