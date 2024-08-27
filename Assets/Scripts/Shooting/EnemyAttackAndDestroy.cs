using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackAndDestroy : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private int damageAmount = 10;
    [SerializeField] private float destroyDelay = 0.5f; // Add this line if you want a delay before destruction

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on " + gameObject.name);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        Debug.Log(playerHealth);
        if (playerHealth != null)
        {
            if (animator != null)
            {
                animator.SetBool("Fire", true);
            }
            playerHealth.TakeDamage(damageAmount);

            // Destroy the enemy
            Destroy(gameObject, destroyDelay); // This will destroy the enemy after a short delay
        }
    }
}
