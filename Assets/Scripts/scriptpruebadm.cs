using UnityEngine;
using UnityEngine.InputSystem;

public class scriptpruebadm : MonoBehaviour {
    public TextAsset csvFile;
    public float zoomCamara;
    public float camPosX;
    public float camPosY;
    public bool primeraAparicion = false;

    public InputActionReference pause;

    private void Start()
    {
        /*
        if (!primeraAparicion)
        {
            DialogueManager.instance.StartDialogue(csvFile, zoomCamara, camPosX, camPosY);
            primeraAparicion = true;
        }
        */
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        DialogueManager.instance.StartDialogue(csvFile, zoomCamara, camPosX, camPosY);
    }
}
