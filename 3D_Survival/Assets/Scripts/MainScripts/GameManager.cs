using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public MonsterPool monsterPool;
    public BulletPool bulletPool;
    public MonsterSpawner monsterSpawner;
    public BulletSpawner bulletSpawner;
    public FieldAttack fieldAttack;
    public Player player;
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
    }
}
