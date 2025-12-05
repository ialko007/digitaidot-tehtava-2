using UnityEngine;

public class PlayerHitSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip spikeHitClip;
    public AudioClip sawHitClip;

    private bool hasDied = false; 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleHit(collision.collider);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        HandleHit(other);
    }

    private void HandleHit(Collider2D other)
    {
        if (hasDied) return;

        if (other.CompareTag("Spike"))
        {
            hasDied = true;
            audioSource.PlayOneShot(spikeHitClip);
            
        }
        else if (other.CompareTag("Saw"))
        {
            hasDied = true;
            audioSource.PlayOneShot(sawHitClip);
            
        }
    }
}
