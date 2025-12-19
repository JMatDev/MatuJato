using UnityEngine;

public class perdiendoLaCordura : MonoBehaviour
{
    public TextAsset csvFile;
    public float zoomCamara;
    public Vector3 camPos;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        DialogueManager.instance.StartDialogue(csvFile, zoomCamara, camPos.x, camPos.y);
        gameObject.SetActive(false);
    }
}
