using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        // Get the Animator component attached to the current GameObject
        animator = GetComponent<Animator>();
        
        // Check if the Animator component exists
        if (animator == null)
        {
            Debug.LogError("Animator component not found on " + gameObject.name);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object is the player
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        Debug.Log(playerHealth);
        if (playerHealth != null)
        {
            if (animator != null)
            {
                animator.SetBool("Fire", true);
            }
            playerHealth.TakeDamage();
        }
    }
}
