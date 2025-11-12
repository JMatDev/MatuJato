using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D RB;
    public Animator animator;
    public InputActionReference move;
    public bool testing = false;

    [HideInInspector] public Vector2 moveInputVector;

    void Start()
    {
        if (testing)
        {
            move.action.actionMap.Enable();
        }
    }

    void Update()
    {
        moveInputVector = Vector4Direcciones();
        SetAnimations();
        Turn();
    }

    Vector2 Vector4Direcciones()
    {
        Vector2 direccion = move.action.ReadValue<Vector2>();
        if (Mathf.Abs(direccion.x) > Mathf.Abs(direccion.y))
            direccion = new Vector2(Mathf.Sign(direccion.x), 0);
        else if (Mathf.Abs(direccion.y) > 0)
            direccion = new Vector2(0, Mathf.Sign(direccion.y));
        return direccion;
    }

    void FixedUpdate()
    {
        RB.linearVelocity = new Vector2(moveInputVector.x * speed, moveInputVector.y * speed);
    }

    void SetAnimations()
    {
        if (moveInputVector != Vector2.zero) animator.SetBool("isRunning", true);
        else animator.SetBool("isRunning", false);

        if (moveInputVector == Vector2.zero) return;
        animator.SetFloat("moveX", moveInputVector.x);
        animator.SetFloat("moveY", moveInputVector.y);
    }

    void Turn()
    {
        if (RB.linearVelocity != Vector2.zero)
        {
            if (moveInputVector.x > 0) transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            else if (moveInputVector.x < 0) transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
    
    public void FirstAnim()
    {
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
    }
}