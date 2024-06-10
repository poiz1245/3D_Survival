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
    public Transform buttonContainer; // buttonContainer �߰�

    public ExperienceManager experienceManager; // ������ �κ�
    public GameObject upgradeButtonPrefab; // ������ �κ�

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
    /////////////////////////////////
    private void Start()
    {
        monsterSpawner.SpawnMonster();


        if (experienceManager == null)
        {
            Debug.LogError("ExperienceManager is not assigned.");
        }

        if (upgradeButtonPrefab == null)
        {
            Debug.LogError("UpgradeButtonPrefab is not assigned.");
        }
    }
    /////////////////////////
    private void Update()


    {
        gameTime += Time.deltaTime;
        
        stageTime+= Time.deltaTime;
        if (stageTime >= 5 && stage != maxStage)
        {
            stage++;
            monsterSpawner.SpawnMonster();
            stageTime = 0;
        }

        //������� ������ �κ�.ȭ�� ����.
        if (experienceManager != null && experienceManager.IsLevelUp())
        {
            Time.timeScale = 0f; // ȭ�� ����
            GenerateUpgradeButtons();
        }

        else if (experienceManager == null)
        {
            Debug.LogError("ExperienceManager is not assigned.");
        }
    }

    void GenerateUpgradeButtons()
    {
        string[] upgradeOptions = { "Max HP ����", "���ݷ� ����", "���� ����", "���� ���� ����" };

        for (int i = 0; i < 3; i++)
        {
            string upgradeType = upgradeOptions[Random.Range(0, upgradeOptions.Length)];

            GameObject buttonObj = Instantiate(upgradeButtonPrefab, Vector3.zero, Quaternion.identity);
            buttonObj.GetComponent<UpgradeButton>().SetUpgradeType(upgradeType);
        }


    }

}



