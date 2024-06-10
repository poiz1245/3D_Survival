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
        GameObject monster = GameManager.Instance.monsterPool.GetMonster(0);
        float stageDelay = spawnDelay - (index * 0.5f);
        switch (index)
        {
            case 0:
                while (true)
                {
                    yield return new WaitForSeconds(stageDelay);
                    monster = GameManager.Instance.monsterPool.GetMonster(0);
                    monster.transform.position = spawnPoints[rnd].position;
                }
            case 1:
                while (true)
                {
                    yield return new WaitForSeconds(stageDelay);
                    monster = GameManager.Instance.monsterPool.GetMonster(2);
                    //monster = GameManager.Instance.monsterPool.GetMonster(1);
                    monster.transform.position = spawnPoints[rnd].position;

                    this.transform.gameObject.SetActive(false);
                }
            case 2:
                while (true)
                {
                    yield return new WaitForSeconds(stageDelay);
                    monster = GameManager.Instance.monsterPool.GetMonster(0);
                    monster = GameManager.Instance.monsterPool.GetMonster(1);
                    monster.transform.position = spawnPoints[rnd].position;
                }
            case 3:
                while (true)
                {
                    yield return new WaitForSeconds(stageDelay);
                    monster = GameManager.Instance.monsterPool.GetMonster(0);
                    monster = GameManager.Instance.monsterPool.GetMonster(1);
                    monster.transform.position = spawnPoints[rnd].position;
                }
            case 4:
                while (true)
                {
                    yield return new WaitForSeconds(stageDelay);
                    monster = GameManager.Instance.monsterPool.GetMonster(0);
                    monster = GameManager.Instance.monsterPool.GetMonster(1);
                    monster.transform.position = spawnPoints[rnd].position;
                }
            case 5:
                while (true)
                {
                    yield return new WaitForSeconds(stageDelay);
                    monster = GameManager.Instance.monsterPool.GetMonster(0);
                    monster = GameManager.Instance.monsterPool.GetMonster(1);
                    monster.transform.position = spawnPoints[rnd].position;
                }
            case 6:
                while (true)
                {
                    yield return new WaitForSeconds(stageDelay);
                    monster = GameManager.Instance.monsterPool.GetMonster(0);
                    monster = GameManager.Instance.monsterPool.GetMonster(1);
                    monster.transform.position = spawnPoints[rnd].position;
                }
            case 7:
                while (true)
                {
                    yield return new WaitForSeconds(stageDelay);
                    monster = GameManager.Instance.monsterPool.GetMonster(0);
                    monster = GameManager.Instance.monsterPool.GetMonster(1);
                    monster.transform.position = spawnPoints[rnd].position;
                }
            case 8:
                while (true)
                {
                    yield return new WaitForSeconds(stageDelay);
                    monster = GameManager.Instance.monsterPool.GetMonster(0);
                    monster = GameManager.Instance.monsterPool.GetMonster(1);
                    monster.transform.position = spawnPoints[rnd].position;
                }
            case 9:
                while (true)
                {
                    yield return new WaitForSeconds(stageDelay);
                    monster = GameManager.Instance.monsterPool.GetMonster(0);
                    monster = GameManager.Instance.monsterPool.GetMonster(1);
                    monster.transform.position = spawnPoints[rnd].position;
                }
            case 10:
                while (true)
                {
                    yield return new WaitForSeconds(stageDelay);
                    monster = GameManager.Instance.monsterPool.GetMonster(2);
                    monster.transform.position = spawnPoints[rnd].position;
                    this.transform.gameObject.SetActive(false);
                }
            default:
                yield return new WaitForSeconds(stageDelay);
                monster = GameManager.Instance.monsterPool.GetMonster(0);
                monster.transform.position = spawnPoints[rnd].position;
                break;
        }
        
    }

}
