using UnityEngine;

public class BotonCamara : MonoBehaviour
{
    public Collider2D triggerArea;
    public CameraRespondTrigger camaraScript;
    private bool izquierda = true; 
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!camaraScript.activated)
            {
                if (izquierda) camaraScript.Girar(-90f, 3.5f);
                else camaraScript.Girar(90f, 3.5f);
                izquierda = !izquierda;
            }       
        }
    }
}
