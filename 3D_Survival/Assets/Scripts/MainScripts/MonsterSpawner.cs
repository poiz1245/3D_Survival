using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    int index;
    [SerializeField] float meleeSpawnDelay;
    [SerializeField] float rangedSpawnDelay;

    [Header("Monster Pool")]
    [SerializeField] Transform[] spawnPoints;

    GameObject meleeMonster;
    GameObject rangedMonster;
    GameObject bossMonster;

    private bool isMaxStage = false;
    
    public void SpawnMonster()
    {
        int stage = GameManager.Instance.stage;
        int maxStage = GameManager.Instance.maxStage;

        if (stage == maxStage)
        {
            isMaxStage = true;
        }

        if (!isMaxStage)
        {
            if (stage == 0 || stage % 2 == 0)
            {
                StartCoroutine(MeleeMonsterSpawn(stage, meleeSpawnDelay));
            }
            else if (stage != 0 && stage % 2 != 0)
            {
                StartCoroutine(MeleeMonsterSpawn(stage, meleeSpawnDelay));
                StartCoroutine(RangedMonsterSpawn(stage, rangedSpawnDelay));
            }
        }

        if (isMaxStage)
        {
            BossMonsterSpawn();
        }

    }

    IEnumerator MeleeMonsterSpawn(int index, float spawnDelay)
    {
        int rnd = Random.Range(0, spawnPoints.Length);
        float stageDelay = spawnDelay - (index * 0.5f);

        if (stageDelay <= 0)
        {
            stageDelay = 0.5f;
        }

        if (index == 0)
        {
            meleeMonster = GameManager.Instance.monsterPool.GetMonster(0);
            meleeMonster.transform.position = spawnPoints[rnd].position;
        }

        while (!isMaxStage)
        {
            yield return new WaitForSeconds(stageDelay);
            meleeMonster = GameManager.Instance.monsterPool.GetMonster(0);
            meleeMonster.transform.position = spawnPoints[rnd].position;
        }

    }

    IEnumerator RangedMonsterSpawn(int index, float spawnDelay)
    {
        int rnd = Random.Range(0, spawnPoints.Length);
        float stageDelay = spawnDelay - (index * 0.5f);

        if (stageDelay <= 0)
        {
            stageDelay = 0.5f;
        }

        while (!isMaxStage)
        {
            yield return new WaitForSeconds(stageDelay);
            rangedMonster = GameManager.Instance.monsterPool.GetMonster(1);
            rangedMonster.transform.position = spawnPoints[rnd].position;
        }
    }

    void BossMonsterSpawn()
    {
        int rnd = Random.Range(0, spawnPoints.Length);

        bossMonster = GameManager.Instance.monsterPool.GetMonster(2);
        bossMonster.transform.position = spawnPoints[rnd].position;
    }
}