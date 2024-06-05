using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] int index;
    [SerializeField] float spawnDelay;

    [Header("Monster Pool")]
    [SerializeField] Transform[] spawnPoints;

    void Start()
    {
        StartCoroutine(MonsterSpawn(0, spawnDelay));
    }

    IEnumerator MonsterSpawn(int index, float spawnDelay)
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);
            int rnd = Random.Range(0, spawnPoints.Length);
            GameObject monster = GameManager.Instance.monsterPool.GetMonster(index);
            monster.transform.position = spawnPoints[rnd].position;
        }
    }
}
