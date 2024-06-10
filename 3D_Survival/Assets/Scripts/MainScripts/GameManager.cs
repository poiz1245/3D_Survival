using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public MonsterPool monsterPool;
    public MonsterSpawner monsterSpawner;
    public BulletPool bulletPool;
    public DropObjectPool dropObjectPool;
    public Player player;

    [SerializeField] int maxStage;


    float stageTime;


    public float gameTime { get; private set; }
    public int stage { get; private set; }
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

    private void Start()
    {
        monsterSpawner.SpawnMonster();
    }
    private void Update()
    {
        gameTime += Time.deltaTime;
        
        stageTime+= Time.deltaTime;
        if (stageTime >= 5 && stage != maxStage)
        {
            stage++;
            monsterSpawner.SpawnMonster();
            stageTime = 0;
            print(stage);
        }
    }
}
