using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemies : MonoBehaviour
{
    public GameObject enemyPrefab;
    // Start is called before the first frame update
    private Transform playerTransform;

    private float timeElapsed;
    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        Vector3 playerCurrentPos = playerTransform.position;
        playerCurrentPos.z += Random.Range(30.0f, 40.0f);
        playerCurrentPos.x += Random.Range(-30.0f, 30.0f);
        
        if (timeElapsed >= 1.5) {
            
            GameObject instantiatedEnemy = Instantiate(
                enemyPrefab,
                playerCurrentPos,
                transform.rotation
            );
            FollowScript followScript = instantiatedEnemy.GetComponent<FollowScript>();
            if (followScript != null)
            {
                followScript.targetObj = playerTransform;
            }
            timeElapsed = 0;
        }
    }
}
