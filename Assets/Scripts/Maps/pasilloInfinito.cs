using UnityEngine;

public class pasilloInfinito : MonoBehaviour
{
    public BoxCollider2D triggerOtroLado;
    public bool ignorar = false;
    [SerializeField] private Vector3 newPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !ignorar)
        {
            triggerOtroLado.GetComponent<pasilloInfinito>().ignorar = true;
            collision.transform.position = newPosition;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ignorar = false;
        }
    }
}
