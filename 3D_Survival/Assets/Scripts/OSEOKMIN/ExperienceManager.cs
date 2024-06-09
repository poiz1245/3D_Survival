using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ExperienceManager : MonoBehaviour
{
    public float experience;
    public float experienceToLevelUp;
    public Slider experienceBar;
    public GameObject upgradeButtonPrefab;
    public Transform buttonContainer;

    public int currentExperience;
    public int maxExperience;

    // 가상의 IsLevelUp 메소드 구현 (예시로 경험치 비교)
    public bool IsLevelUp()
    {
        return currentExperience >= maxExperience;
    }

    //private void Update()
    //{
    //    if (experience >= experienceToLevelUp)
    //    {
    //        experience = 0; // 경험치 초기화
    //        Time.timeScale = 0f; // 게임 일시 정지
    //        GenerateUpgradeButtons();
    //    }
    //}

    public void AddExperience(float amount)
    {
        experience += amount;
        experienceBar.value = experience / experienceToLevelUp;
    }

    void GenerateUpgradeButtons()
    {
        if (upgradeButtonPrefab == null || buttonContainer == null)
        {
            Debug.LogError("upgradeButtonPrefab or buttonContainer is not assigned.");
            return;
        }

        string[] upgradeOptions = { "Max HP 증가", "공격력 증가", "방어력 증가", "공격 범위 증가" };
        List<string> selectedUpgrades = new List<string>(upgradeOptions);

        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(0, selectedUpgrades.Count);
            string upgradeType = selectedUpgrades[randomIndex];
            selectedUpgrades.RemoveAt(randomIndex);

            GameObject buttonObj = Instantiate(upgradeButtonPrefab, buttonContainer);
            UpgradeButton upgradeButton = buttonObj.GetComponent<UpgradeButton>();
            upgradeButton.SetUpgradeType(upgradeType);
        }
    }
}
