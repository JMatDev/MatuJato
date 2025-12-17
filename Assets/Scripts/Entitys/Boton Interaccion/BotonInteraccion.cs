using UnityEngine;

public class BotonInteraccion : MonoBehaviour
{
    public GameObject botonInteraccion;
    public Animator animatorBoton;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animatorBoton.SetTrigger("Aparecer");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animatorBoton.SetTrigger("Desaparecer");
        }
    }

    public void OcultarBoton()
    {
        animatorBoton.SetTrigger("Desaparecer");
    }
}
