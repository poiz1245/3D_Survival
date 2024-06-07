using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public MonsterPool monsterPool;
    public BulletPool bulletPool;
    public DropObjectPool dropObjectPool;
    public Player player;

    [SerializeField] int maxStage;
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

    private void Update()
    {
        gameTime += Time.deltaTime;

        float stageTime = 0;
        stageTime += Time.deltaTime;

        if (stageTime >= 30 && stage != maxStage)
        {
            stage++;
            stageTime = 0;
        }
    }
}
