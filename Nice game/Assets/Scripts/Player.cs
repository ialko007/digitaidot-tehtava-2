using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    bool isRunning = false;
    Animator animator;
    Rigidbody2D rb;
    PlayerInput input;
    public Canvas canvasToHide;

    public float jumpImpulse = 20.0f;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        input = gameObject.GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("xVelocity", isRunning ? 1.0f : 0.0f);
        if (input.actions["Jump"].inProgress)
        {
            rb.gravityScale = 1.0f;
        }
        else
        {
            rb.gravityScale = 3.0f;
        }
    }
    
    void OnJump()
    {
        if (!isRunning)
        {
            isRunning = true;
            canvasToHide.enabled = false;
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, 1.0f);
            if (hit)
            {
                Debug.Log("Hit object: " + hit.transform.name);
                Debug.Log("On the ground - can jump!");
                rb.linearVelocityY = 0.0f;
                rb.AddForce(transform.up * jumpImpulse, ForceMode2D.Impulse);
            }
            else
            {
                Debug.Log("Bro, you're not even on the ground!");
            }
        }
    }
}
