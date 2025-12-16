using UnityEngine;
using UnityEngine.InputSystem;

public class Interactuar : MonoBehaviour
{
    public InputActionReference interact;

    private bool isInRange = false;
    private Collider2D triggerCollider;

    void Start()
    {
        interact.action.performed += OnInteract;
    }

    void OnInteract(InputAction.CallbackContext context)
    {
        if (isInRange)
        {
            triggerCollider.GetComponent<DialogueTrigger>().TriggerDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dialogue")) isInRange = true;
        triggerCollider = collision;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Dialogue")) isInRange = false;
        triggerCollider = null;
    }
}
