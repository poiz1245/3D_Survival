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

        //player.OnPlayerLevelChanged += PlayerLevelUp;

    }
    /*private void PlayerLevelUp(int level)
    {
        Time.timeScale = 0;
    }*/
    private void Start()
    {
        monsterSpawner.SpawnMonster();
    }
    private void Update()
    {
        gameTime += Time.deltaTime;

        stageTime += Time.deltaTime;
        if (stageTime >= 5 && stage != maxStage)
        {
            stage++;
            monsterSpawner.SpawnMonster();
            stageTime = 0;
        }

        // 여기부터 수정된 부분. 화면 정지.
        //if (experienceManager != null && experienceManager.IsLevelUp())
        //{
        //    Time.timeScale = 0f; // 화면 정지
        //    GenerateUpgradeButtons();
        //}
    }
}



