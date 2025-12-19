using UnityEngine;
using UnityEngine.UIElements;

public class continuarAnimacion : MonoBehaviour
{
    public Animator animator;
    public Animator llave;
    public Animator logo;
    public Animator Texto;
    public GameObject fondoTapa;
    
    public void ContinuarAnimacion()
    {
        animator.SetTrigger("Aparecer");
    }

    public void ContinuarAnimacion2()
    {
        llave.SetTrigger("Aparecer");
    }

    public void ContinuarAnimacion3()
    {
        fondoTapa.SetActive(true);
        animator.SetTrigger("LLave");
    }

    public void ContinuarAnimacion4()
    {
        logo.SetTrigger("Logo");
        Texto.SetTrigger("Aparecer");
    }
}
