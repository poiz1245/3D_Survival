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
            print("���� ��������:" + stage);
            if (stage == 0 || stage % 2 == 0)
            {
                StartCoroutine(MeleeMonsterSpawn(stage, meleeSpawnDelay));
            }
            else if (stage % 2 != 0)
            {
                StartCoroutine(RangedMonsterSpawn(stage, rangedSpawnDelay));
            }
        }

        if (isMaxStage)
        {
            print("�ƽ� ��������");
            StartCoroutine(BossMonsterSpawn(stage, 0));
        }

    }

    IEnumerator MeleeMonsterSpawn(int index, float spawnDelay)
    {
        int rnd = Random.Range(0, spawnPoints.Length);
        float stageDelay = spawnDelay - (index * 0.5f);

        while (true)
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

        while (true)
        {
            yield return new WaitForSeconds(stageDelay);
            rangedMonster = GameManager.Instance.monsterPool.GetMonster(1);
            rangedMonster.transform.position = spawnPoints[rnd].position;
        }
    }

    IEnumerator BossMonsterSpawn(int index, float spawnDelay)
    {
        print("�������ͼ�ȯ");
        int rnd = Random.Range(0, spawnPoints.Length);
        float stageDelay = spawnDelay - (index * 0.5f);

        yield return new WaitForSeconds(stageDelay);
        bossMonster = GameManager.Instance.monsterPool.GetMonster(2);
        bossMonster.transform.position = spawnPoints[rnd].position;
    }
}