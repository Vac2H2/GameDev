using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemies : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject player;
    // Start is called before the first frame update

    private float timeElapsed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        Vector3 playerCurrentPos = player.transform.position;
        playerCurrentPos.z += Random.Range(30.0f, 40.0f);
        playerCurrentPos.x += Random.Range(-30.0f, 30.0f);
        
        if (timeElapsed >= 1.5) {
            Instantiate(
                enemyPrefab,
                playerCurrentPos,
                transform.rotation
            );
            timeElapsed = 0;
        }
    }
}
