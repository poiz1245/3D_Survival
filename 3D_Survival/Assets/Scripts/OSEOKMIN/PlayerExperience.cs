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
            // ���� ���� ����
            return;
        }
        // ����ִ� ���� ����
    }

    void Die()
    {
        DropExperience();
        DropMoney();
        isDead = true;

        // ������ ��ư ����
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
            "���� ����",
            "Max HP ����",
            "���ݷ� ����",
            "���� ���� ����"
        };

        for (int i = 0; i < 3; i++)
        {
            string upgradeType = upgradeOptions[Random.Range(0, upgradeOptions.Count)];

            GameObject buttonObj = Instantiate(upgradeButtonPrefab, transform.position + Vector3.up * i, Quaternion.identity);
            //buttonObj.GetComponent<UpgradeButton>().SetUpgradeType(upgradeType);
        }
    }
}

