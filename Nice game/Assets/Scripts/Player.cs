using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    bool isRunning = false;
    Animator animator;
    Rigidbody2D rb;
    public Canvas canvasToHide;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("xVelocity", isRunning ? 1.0f : 0.0f);
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
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, 1.5f);
            if (hit)
            {
                Debug.Log("Hit object: " + hit.transform.name);
                Debug.Log("On the ground - can jump!");
                rb.linearVelocityY = 0.0f;
                rb.AddForce(transform.up * 20.0f, ForceMode2D.Impulse);
            }
            else
            {
                Debug.Log("Bro, you're not even on the ground!");
            }
        }
    }
}
