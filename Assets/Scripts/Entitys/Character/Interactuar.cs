using UnityEngine;
using UnityEngine.InputSystem;

public class Interactuar : MonoBehaviour
{
    public InputActionReference interact;

    private bool isInRange = false;
    private Collider2D triggerCollider;
    private bool isDialouge = false;
    private bool isDoor = false;

    void Start()
    {
        interact.action.performed += OnInteract;
    }

    void OnInteract(InputAction.CallbackContext context)
    {
        if (isInRange)
        {
            if (isDialouge) triggerCollider.GetComponent<DialogueTrigger>().TriggerDialogue();
            if (isDoor) triggerCollider.GetComponent<TransicionNiveles>().CambiarNivel();
            triggerCollider.GetComponent<BotonInteraccion>().OcultarBoton();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dialogue") || collision.CompareTag("Door")) 
        {
            triggerCollider = collision;
            isInRange = true;
            if (collision.CompareTag("Dialogue")) isDialouge = true;
            if (collision.CompareTag("Door")) isDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Dialogue") || collision.CompareTag("Door"))
        {
            triggerCollider = null;
            isInRange = false;
            isDialouge = false;
            isDoor = false;
        }
        
    }

    public void Respawn()
    {
        GetComponentInChildren<Respawn>().RespawnCharacter();
    }
}
