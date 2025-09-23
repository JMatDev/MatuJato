using UnityEngine;

public class IntroductoryLevel_Manager : MonoBehaviour {
    [SerializeField] private DialogueManager levelDialogues;
    [SerializeField] private BoxCollider2D hotelBoxCollider;

    void Start() {
        levelDialogues.StartDialogue(0, 4);        
    }

    

}
