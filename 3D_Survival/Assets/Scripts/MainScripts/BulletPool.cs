using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] GameObject[] bulletPrefabs;

    List<GameObject>[] bulletPool;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        bulletPool = new List<GameObject>[bulletPrefabs.Length];

        for (int i = 0; i < bulletPool.Length; i++)
        {
            bulletPool[i] = new List<GameObject>();
        }
    }

    public GameObject GetBullet(int index)
    {
        GameObject select = null;

        foreach (GameObject obj in bulletPool[index])
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
            select = Instantiate(bulletPrefabs[index], transform);
            bulletPool[index].Add(select);
            print("¼ß");
        }

        return select;
    }
}