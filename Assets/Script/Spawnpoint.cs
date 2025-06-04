using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Manager.Instance.SetSpawnPoint(transform.position);
            Debug.Log("Spawn point set!");
        }
    }
}
