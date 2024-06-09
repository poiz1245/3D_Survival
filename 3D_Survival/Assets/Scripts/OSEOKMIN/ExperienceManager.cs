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

    // ������ IsLevelUp �޼ҵ� ���� (���÷� ����ġ ��)
    public bool IsLevelUp()
    {
        return currentExperience >= maxExperience;
    }

    //private void Update()
    //{
    //    if (experience >= experienceToLevelUp)
    //    {
    //        experience = 0; // ����ġ �ʱ�ȭ
    //        Time.timeScale = 0f; // ���� �Ͻ� ����
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

        string[] upgradeOptions = { "Max HP ����", "���ݷ� ����", "���� ����", "���� ���� ����" };
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
