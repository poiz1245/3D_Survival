using UnityEngine;
using System.Collections.Generic;

public class Goblin : MonoBehaviour
{
    public GameObject expPrefab;
    public GameObject moneyPrefab;
    public GameObject upgradeButtonPrefab;

    public int expAmount = 10;
    public int moneyAmount = 20;

    private bool isDead = false;

    void Update()
    {
        if (isDead)
        {
            // 죽은 후의 로직
            return;
        }
        // 살아있는 중의 로직
    }

    void Die()
    {
        DropExperience();
        DropMoney();
        isDead = true;

        // 레벨업 버튼 생성
        GenerateUpgradeButtons();
    }

    void DropExperience()
    {
        GameObject expObj = Instantiate(expPrefab, transform.position, Quaternion.identity);
        expObj.GetComponent<ExpObject>().SetAmount(expAmount);
    }

    void DropMoney()
    {
        Instantiate(moneyPrefab, transform.position, Quaternion.identity);
    }

    void GenerateUpgradeButtons()
    {
        List<string> upgradeOptions = new List<string>()
        {
            "방어력 증가",
            "Max HP 증가",
            "공격력 증가",
            "공격 범위 증가"
        };

        for (int i = 0; i < 3; i++)
        {
            string upgradeType = upgradeOptions[Random.Range(0, upgradeOptions.Count)];

            GameObject buttonObj = Instantiate(upgradeButtonPrefab, transform.position + Vector3.up * i, Quaternion.identity);
            //buttonObj.GetComponent<UpgradeButton>().SetUpgradeType(upgradeType);
        }
    }
}

