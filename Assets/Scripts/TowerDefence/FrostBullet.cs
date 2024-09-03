using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostBullet : MonoBehaviour
{
    public GameObject hitEffect;
    public float effectScale = 0.5f;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float freezeTime = 1.0f;
    [SerializeField] private float slowAmount = 0.2f;
    [SerializeField] private float bulletSpeed = 4f;


    private Transform target;


    private void FixedUpdate()
    {
        if (!target)
        {
            Destroy(gameObject);
            return;
        }

        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        EnemyMovement enemy = other.gameObject.GetComponent<EnemyMovement>();
        if (hitEffect != null) {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            effect.transform.localScale *= effectScale;

            Destroy(effect, 2);
        }
        if (enemy != null)
        {
            StartCoroutine(ApplyFreeze(enemy));
        }
        Destroy(gameObject); // Destroy the bullet after hitting an enemy
    }

    private IEnumerator ApplyFreeze(EnemyMovement enemy)
    {
        float reducedSpeed = enemy.GetSpeed() * slowAmount;

        // Reduce speed
        enemy.UpdateSpeed(reducedSpeed);

        // Wait for the freeze duration
        yield return new WaitForSeconds(freezeTime);

        // Reset speed
        enemy.ResetSpeed();
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

}
