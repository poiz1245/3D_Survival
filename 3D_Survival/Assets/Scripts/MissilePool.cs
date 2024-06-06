using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilePool : MonoBehaviour
{
    [SerializeField] GameObject[] missilePrefabs;
    List<GameObject>[] missilePool;
    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        missilePool = new List<GameObject>[missilePrefabs.Length];

        for (int i = 0; i < missilePool.Length; i++)
        {
            missilePool[i] = new List<GameObject>();
        }
    }

    public GameObject GetMissile(int index)
    {
        GameObject select = null;

        foreach (GameObject obj in missilePool[index])
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
            select = Instantiate(missilePrefabs[index], transform);
            missilePool[index].Add(select);
        }

        return select;
    }
}
