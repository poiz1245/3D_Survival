using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawner : MonoBehaviour
{
    [SerializeField] float spawnDelay;
    float spawnTime;

    private void FixedUpdate()
    {
        spawnTime -= Time.fixedDeltaTime;

        if (spawnTime <= 0) 
        {
            MissileSpawn(1);
            spawnTime = spawnDelay;
        }
    }

    public void MissileSpawn(int index)
    {
        GameObject missile = GameManager.Instance.bulletPool.GetBullet(index);
        missile.transform.position = transform.position;
    }
}