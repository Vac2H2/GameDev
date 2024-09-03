using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float minimumSpeed = 0.5f;

    private Transform target;
    private int pathIndex = 0;

    private float baseSpeed;

    private void Start()
    {
        baseSpeed = moveSpeed;
        if (LevelManager.main.path.Length > 0)
        {
            target = LevelManager.main.path[0];
        }
        else
        {
            Debug.LogError("Path array is empty or not int");
        }
    }

    private void Update()
    {
        if (LevelManager.main.path.Length == 0)
        {
            return;
        }

        if(UnityEngine.Vector2.Distance(target.position, transform.position) <= 0.1f){
            pathIndex++;

            if(pathIndex == LevelManager.main.path.Length){
                //EnemySpawner.onEnemyDestroy.Invoke();
                LevelManager.main.GameOver();
                //FindObjectOfType<GameOverManager>().ShowGameOver();
                Destroy(gameObject);
                return;
            } else{
                target = LevelManager.main.path[pathIndex];
            }
        }
    }

    private void FixedUpdate() {
        UnityEngine.Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * moveSpeed;
    }

    public void UpdateSpeed(float newSpeed) {
        moveSpeed = Mathf.Max(newSpeed, minimumSpeed);
    }

    public void ResetSpeed() {
        moveSpeed = baseSpeed;
    }


    public float GetSpeed()
    {
        return moveSpeed;
    }
}
