using UnityEngine;

public class scriptpruebadm : MonoBehaviour
{
    public DialogueManager2 dialogueManager;
    public int idDialogo;
    public float zoomCamara;
    public float camPosX;
    public float camPosY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        dialogueManager.StartDialogue(idDialogo, zoomCamara, camPosX, camPosY);
    }
}
