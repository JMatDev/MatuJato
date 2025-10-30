using UnityEngine;

public class BotonCamara : MonoBehaviour
{
    public Collider2D triggerArea;
    public CameraRespondTrigger camaraScript;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            camaraScript.Girar();
        }
    }
}
