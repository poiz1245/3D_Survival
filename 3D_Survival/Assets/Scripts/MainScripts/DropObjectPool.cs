using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropObjectPool : MonoBehaviour
{
    [SerializeField] GameObject[] dropObjectPrefabs;

    List<GameObject>[] dropObjectPool;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        dropObjectPool = new List<GameObject>[dropObjectPrefabs.Length];

        for (int i = 0; i < dropObjectPool.Length; i++)
        {
            dropObjectPool[i] = new List<GameObject>();
        }
    }

    public GameObject GetDropObject(int index)
    {
        GameObject select = null;

        foreach (GameObject obj in dropObjectPool[index])
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
            select = Instantiate(dropObjectPrefabs[index], transform);
            dropObjectPool[index].Add(select);
        }

        return select;
    }
}