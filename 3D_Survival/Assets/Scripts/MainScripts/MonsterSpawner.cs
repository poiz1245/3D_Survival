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
        int rnd = Random.Range(0, spawnPoints.Length);

        if (stage == 0)
        {
            meleeMonster = GameManager.Instance.monsterPool.GetMonster(0);
            meleeMonster.transform.position = spawnPoints[rnd].position;
        }

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
        //int[] rnd = new int[5];
        int rnd;
        float stageDelay = spawnDelay - (index * 0.5f);

        if (stageDelay <= 0)
        {
            stageDelay = 0.5f;
        }

        if (index == 0)
        {
            meleeMonster = GameManager.Instance.monsterPool.GetMonster(0);
            meleeMonster.transform.position = spawnPoints[0].position;
        }

        while (!isMaxStage)
        {
            yield return new WaitForSeconds(stageDelay);
            for (int i = 0; i < 5; i++)
            {
                rnd = Random.Range(0, spawnPoints.Length);
                meleeMonster = GameManager.Instance.monsterPool.GetMonster(0);
                meleeMonster.transform.position = spawnPoints[rnd].position;
            }

/*            meleeMonster = GameManager.Instance.monsterPool.GetMonster(0);
            meleeMonster = GameManager.Instance.monsterPool.GetMonster(0);
            meleeMonster = GameManager.Instance.monsterPool.GetMonster(0);
            meleeMonster = GameManager.Instance.monsterPool.GetMonster(0);
            meleeMonster = GameManager.Instance.monsterPool.GetMonster(0);
            meleeMonster.transform.position = spawnPoints[rnd[1]].position;
            meleeMonster.transform.position = spawnPoints[rnd[2]].position;
            meleeMonster.transform.position = spawnPoints[rnd[3]].position;
            meleeMonster.transform.position = spawnPoints[rnd[4]].position;*/
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