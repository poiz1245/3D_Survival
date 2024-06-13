using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject buttonPanel;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject gameClearPanel;
    [SerializeField] Slider expBar;
    [SerializeField] Slider HpBar;
    [SerializeField] Text timer;
    [SerializeField] GameObject playerWinObject;
    [SerializeField] GameObject playerDiedObject;

    [SerializeField] List<GameObject> button;
    [SerializeField] Transform[] spots;
    public GameObject[] inventoryRawImage;
    public Text[] upGradeText;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }
    private void Start()
    {
        GameManager.Instance.player.OnPlayerLevelChanged += PlayerLevelUp;
        GameManager.Instance.player.OnPlayerStateChanged += GameOver;
        inventoryRawImage[0].SetActive(true);
    }
    private void Update()
    {
        TimerSetting();

        float expPercent = GameManager.Instance.player.currentExperience / GameManager.Instance.player.maxExperience;
        float hpPercent = GameManager.Instance.player.hp / GameManager.Instance.player.maxHp;
        expBar.value = expPercent;
        HpBar.value = hpPercent;
    }
    private void TimerSetting()
    {
        string timeText = string.Format("{0:00}:{1:00}:{2:00}", GameManager.Instance.timeSpan.Minutes, GameManager.Instance.timeSpan.Seconds, (int)GameManager.Instance.timeSpan.Milliseconds / 10);
        timer.text = timeText;
    }

    public void SetText(int index, string text)
    {
        upGradeText[index].text = text;
    }
    public void GameClear(bool bossDead)
    {
        SoundManager.Instance.GameClear();
        buttonPanel.SetActive(!bossDead);
        gameClearPanel.SetActive(bossDead);
        playerWinObject.SetActive(true);
        Time.timeScale = 0f;
    }
    void GameOver(bool isDead)
    {
        buttonPanel.SetActive(!isDead);
        gameOverPanel.SetActive(isDead);
        playerDiedObject.SetActive(true);
        Time.timeScale = 0f;
    }
    private void PlayerLevelUp(int level)
    {
        List<int> getIndicesToRemove = GetIndicesToRemove();

        for (int i = getIndicesToRemove.Count - 1; i >= 0; i--)
        {
            button.RemoveAt(getIndicesToRemove[i]);
        }

        buttonPanel.SetActive(true);

        List<int> randomNumbers = GetRandomNumber();
        for (int i = 0; i < 3; i++)
        {
            button[randomNumbers[i]].transform.position = spots[i].position;
            button[randomNumbers[i]].SetActive(true);
        }

        Time.timeScale = 0;
    }
    List<int> GetIndicesToRemove()
    {
        List<int> indicesToRemove = new();

        for (int i = 0; i < button.Count; i++) //Count 8
        {
            button[i].SetActive(false);

            if (i < WeaponManager.instance.weaponsList.Count) //Length 4
            {
                if (WeaponManager.instance.weaponsList[i].level == 4)
                {
                    indicesToRemove.Add(i);
                    WeaponManager.instance.weaponsList.RemoveAt(i); //한번 조건에 걸려서 제거된 무기는 다시 걸리지 않도록 삭제
                }
            }
        }

        return indicesToRemove;
    }
    List<int> GetRandomNumber()
    {
        List<int> numbers = new();
        List<int> result = new();

        for (int i = 0; i < button.Count; i++)
        {
            numbers.Add(i);
        }

        for (int i = 0; i < 3; ++i)
        {
            int index = Random.Range(0, numbers.Count);
            result.Add(numbers[index]);
            numbers.RemoveAt(index);
        }

        return result;
    }
}