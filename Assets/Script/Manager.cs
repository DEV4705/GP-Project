using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager Instance;

    private Vector3 spawnPoint;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetSpawnPoint(Vector3 position)
    {
        spawnPoint = position;
    }

    public void RespawnPlayer(GameObject player)
    {
        player.transform.position = spawnPoint;
    }
}
