using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockEffect : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float expandSpeed = 5f; // Speed at which the circle expands
    [SerializeField] private float damage = 10f; // Damage dealt by the shock
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private float duration = 1f;

    private float radius;
    private float currentRadius;
    private HashSet<Collider2D> damagedEnemies = new HashSet<Collider2D>(); // Track damaged enemies
    private float startTime;

    public void Initialize(float radius, float damage, float duration)
    {
        this.radius = radius;
        this.damage = damage;
        this.duration = duration;
        this.currentRadius = 0f;
        this.startTime = Time.time;
    }

    private void Update()
    {
        if (currentRadius < radius)
        {
            currentRadius += expandSpeed * Time.deltaTime;
            transform.localScale = Vector3.one * currentRadius * 2;

        }
        // Expand the circle
        //transform.localScale += Vector3.one * expandSpeed * Time.deltaTime;

        // Apply damage to enemies
        ApplyDamageToEnemies();

        // Optionally destroy the shock effect after its duration
        if (Time.time - startTime >= duration)
        {
            Destroy(gameObject);
        }
    }

    private void ApplyDamageToEnemies()
    {
        // Get all enemies within the current radius
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, transform.localScale.x / 2, enemyMask);

        foreach (var enemy in enemies)
        {
            // Only apply damage if the enemy has not been damaged yet
            if (!damagedEnemies.Contains(enemy))
            {
                Health enemyHealth = enemy.GetComponent<Health>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage((int)damage); // Cast float to int
                    damagedEnemies.Add(enemy); // Mark this enemy as damaged
                }
            }
        }
    }
}
