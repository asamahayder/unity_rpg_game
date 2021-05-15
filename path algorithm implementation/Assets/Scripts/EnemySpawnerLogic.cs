using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerLogic : MonoBehaviour
{

    public GameObject enemyToSpawn;
    public int maxEnemyCount;
    public float spawnAreaRadius;
    private int currentEnemyCount = 0;

    private Vector3 startPosition;
    private float z1, z2, x1, x2;
    private Grid grid;

    public int CurrentEnemyCount
    {
        set
        {
            if(value < currentEnemyCount)
            {
                StartCoroutine(spawnEnemyRoutine());
            }
            currentEnemyCount = value;
        }
        get
        {
            return currentEnemyCount;
        }
    }


    void Start()
    {

        grid = GameObject.Find("Grid").GetComponent<Grid>();

        startPosition = transform.position;
        z1 = startPosition.z - spawnAreaRadius;
        z2 = startPosition.z + spawnAreaRadius;
        x1 = startPosition.x - spawnAreaRadius;
        x2 = startPosition.x + spawnAreaRadius;

        for(int i = 0; i < maxEnemyCount; i++)
        {
            spawnEnemy();
        }

        
    }

    void spawnEnemy()
    {
        if (currentEnemyCount < maxEnemyCount)
        {
            Vector3 randomPositionWithinRoamArea;
            Node node;
            do //we need to check if the spawned location is walkable
            {
                randomPositionWithinRoamArea = new Vector3(Random.Range(x1, x2), 0, Random.Range(z1, z2));
                node = grid.nodeFromWorldPoint(randomPositionWithinRoamArea);
            }
            while (!node.walkable);
            

            GameObject enemy = Instantiate(enemyToSpawn, randomPositionWithinRoamArea, Quaternion.identity);
            enemy.transform.parent = gameObject.transform;
            CurrentEnemyCount++;
        }
    }

    IEnumerator spawnEnemyRoutine()
    {
        yield return new WaitForSeconds(5);
        spawnEnemy();
        
    }
}
