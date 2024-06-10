using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPool : MonoBehaviour
{
    [SerializeField] GameObject[] monsterPrefabs;

    List<GameObject>[] monsterPool;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        monsterPool = new List<GameObject>[monsterPrefabs.Length];

        for (int i = 0; i < monsterPool.Length; i++)
        {
            monsterPool[i] = new List<GameObject>();
        }
    }

    public GameObject GetMonster(int index)
    {
        GameObject select = null;
        foreach (GameObject obj in monsterPool[index])
        {
            if (!obj.activeSelf)
            {
                select = obj;
                select.SetActive(true);
                break;
            }
        }

        if (!select)
        {
            select = Instantiate(monsterPrefabs[index], transform);
            monsterPool[index].Add(select);
        }

        return select;
    }
}