using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;

    [SerializeField]
    private float MinSpawnTime;

    [SerializeField]
    private float MaxSpawnTime;

    private float TimeUntilSpawn;

    private void Update()
    {
        TimeUntilSpawn -= Time.deltaTime;

        if(TimeUntilSpawn <= 0)
        {
            Instantiate(Enemy, transform.position, Quaternion.identity);
            SetTimeUntilSpawn();
        }
    }

    private void SetTimeUntilSpawn()
    {
        TimeUntilSpawn = Random.Range(MinSpawnTime, MaxSpawnTime);
    }
}
