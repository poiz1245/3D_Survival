using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawner : MonoBehaviour
{
    [SerializeField] float spawnDelay;

    float spawnTime;
    int count = 0;
    bool findTarget;

    public GameObject missilePrefab; // 미사일 프리팹
    public Transform spawnPoint; // 미사일 발사 위치

    private void FixedUpdate()
    {
        spawnTime -= Time.fixedDeltaTime;
        findTarget = GameManager.Instance.player.findTarget;

        if (spawnTime <= 0 && findTarget) // 몬스터 끼리 겹쳐서 타겟이 안맞아서 스캔이 안됨
        {
            MissileSpawn(0);
            spawnTime = spawnDelay;
        }
    }

    public void MissileSpawn(int index)
    {
        //Debug.Log("미사일 스폰");
        //GameObject missile = GameManager.Instance.missilePool.GetMissile(index);
        //missile.transform.position = transform.position;

        GameObject missile = Instantiate(missilePrefab, spawnPoint.position, spawnPoint.rotation);
        Missile missileComponent = missile.GetComponent<Missile>();
    }
}