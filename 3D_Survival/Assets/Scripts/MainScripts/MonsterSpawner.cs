using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    int index;
    [SerializeField] float spawnDelay;

    [Header("Monster Pool")]
    [SerializeField] Transform[] spawnPoints;

    public void SpawnMonster()
    {
        StartCoroutine(StageMonsterSpawn(GameManager.Instance.stage, spawnDelay));
    }
    IEnumerator StageMonsterSpawn(int index, float spawnDelay)
    {
        int rnd = Random.Range(0, spawnPoints.Length);
        GameObject meleeMonster = GameManager.Instance.monsterPool.GetMonster(0);
        GameObject rangedMonster = GameManager.Instance.monsterPool.GetMonster(1);
        float stageDelay = spawnDelay - (index * 0.5f);
        switch (index)
        {
            case 0:
                while (true)
                {
                    yield return new WaitForSeconds(stageDelay);
                    meleeMonster = GameManager.Instance.monsterPool.GetMonster(0);
                    meleeMonster.transform.position = spawnPoints[rnd].position;
                }
            case 1:
                while (true)
                {
                    yield return new WaitForSeconds(stageDelay);
                    meleeMonster = GameManager.Instance.monsterPool.GetMonster(0);
                    //rangedMonster = GameManager.Instance.monsterPool.GetMonster(1);
                    meleeMonster.transform.position = spawnPoints[rnd].position;
                    //rangedMonster.transform.position = spawnPoints[rnd].position;
                }
            case 2:
                while (true)
                {
                    yield return new WaitForSeconds(stageDelay);
                    meleeMonster = GameManager.Instance.monsterPool.GetMonster(0);
                    rangedMonster = GameManager.Instance.monsterPool.GetMonster(1);
                    meleeMonster.transform.position = spawnPoints[rnd].position;
                    rangedMonster.transform.position = spawnPoints[rnd].position;
                }
            case 3:
                while (true)
                {
                    yield return new WaitForSeconds(stageDelay);
                    meleeMonster = GameManager.Instance.monsterPool.GetMonster(0);
                    //rangedMonster = GameManager.Instance.monsterPool.GetMonster(1);
                    meleeMonster.transform.position = spawnPoints[rnd].position;
                    //rangedMonster.transform.position = spawnPoints[rnd].position;
                }
            case 4:
                while (true)
                {
                    yield return new WaitForSeconds(stageDelay);
                    meleeMonster = GameManager.Instance.monsterPool.GetMonster(0);
                    rangedMonster = GameManager.Instance.monsterPool.GetMonster(1);
                    meleeMonster.transform.position = spawnPoints[rnd].position;
                    rangedMonster.transform.position = spawnPoints[rnd].position;
                }
            case 5:
                while (true)
                {
                    yield return new WaitForSeconds(stageDelay);
                    meleeMonster = GameManager.Instance.monsterPool.GetMonster(0);
                    //rangedMonster = GameManager.Instance.monsterPool.GetMonster(1);
                    meleeMonster.transform.position = spawnPoints[rnd].position;
                    //rangedMonster.transform.position = spawnPoints[rnd].position;
                }
            case 6:
                while (true)
                {
                    yield return new WaitForSeconds(stageDelay);
                    meleeMonster = GameManager.Instance.monsterPool.GetMonster(0);
                    rangedMonster = GameManager.Instance.monsterPool.GetMonster(1);
                    meleeMonster.transform.position = spawnPoints[rnd].position;
                    rangedMonster.transform.position = spawnPoints[rnd].position;
                }
            case 7:
                while (true)
                {
                    yield return new WaitForSeconds(stageDelay);
                    meleeMonster = GameManager.Instance.monsterPool.GetMonster(0);
                    //rangedMonster = GameManager.Instance.monsterPool.GetMonster(1);
                    meleeMonster.transform.position = spawnPoints[rnd].position;
                    //rangedMonster.transform.position = spawnPoints[rnd].position;
                }
            case 8:
                while (true)
                {
                    yield return new WaitForSeconds(stageDelay);
                    meleeMonster = GameManager.Instance.monsterPool.GetMonster(0);
                    rangedMonster = GameManager.Instance.monsterPool.GetMonster(1);
                    meleeMonster.transform.position = spawnPoints[rnd].position;
                    rangedMonster.transform.position = spawnPoints[rnd].position;
                }
            case 9:
                while (true)
                {
                    yield return new WaitForSeconds(stageDelay);
                    meleeMonster = GameManager.Instance.monsterPool.GetMonster(0);
                    //rangedMonster = GameManager.Instance.monsterPool.GetMonster(1);
                    meleeMonster.transform.position = spawnPoints[rnd].position;
                    //rangedMonster.transform.position = spawnPoints[rnd].position;
                }
            default:
                yield return new WaitForSeconds(stageDelay);
                meleeMonster = GameManager.Instance.monsterPool.GetMonster(0);
                meleeMonster.transform.position = spawnPoints[rnd].position;
                break;
        }

    }

}
