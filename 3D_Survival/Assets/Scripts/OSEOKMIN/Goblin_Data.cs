using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_Data : MonoBehaviour
{
    public GameObject experiencePrefab; // 경험치 오브젝트 프리팹
    public GameObject MoneyPrefab; // 돈 오브젝트 프리팹
    public int experienceAmount = 10; // 기본 경험치 양
    public int moneyAmount = 1; // 기본 돈 양

    void Die()
    {
        DropExperience();
        DropMoney();
        gameObject.SetActive(false); // 몬스터 오브젝트 비활성화
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
