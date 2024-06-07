using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_Data : MonoBehaviour
{
    public GameObject experiencePrefab; // ����ġ ������Ʈ ������
    public GameObject MoneyPrefab; // �� ������Ʈ ������
    public int experienceAmount = 10; // �⺻ ����ġ ��
    public int moneyAmount = 1; // �⺻ �� ��

    void Die()
    {
        DropExperience();
        DropMoney();
        gameObject.SetActive(false); // ���� ������Ʈ ��Ȱ��ȭ
    }

    void DropExperience()
    {
        GameObject experience = Instantiate(experiencePrefab, transform.position, Quaternion.identity);
        GoblinExperience expScript = experience.GetComponent<GoblinExperience>();
        if (expScript != null)
        {
            expScript.SetAmount(experienceAmount);
        }
    }

    void DropMoney()
    {
        Instantiate(MoneyPrefab, transform.position, Quaternion.identity);
    }
    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
