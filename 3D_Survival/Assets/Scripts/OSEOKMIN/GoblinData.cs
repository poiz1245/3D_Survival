using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;



public class GoblinData : MonoBehaviour
{
    public int experiencePoints = 10;
    public int moneyDrop = 10;
    public GameObject experiencePrefab;
    public GameObject moneyPrefab;

    //public GameObject 

    
    
    public void Die()
    {
        moneyDrop = 10;
        experiencePoints = 10;
        DestroyObject(gameObject);
    }
    void DropExperience ()
    {
        GameObject experience = Instantiate(experiencePrefab, transform.position, Quaternion.identity);
        ExperiencePickup experiencePickup = experience.GetComponent<ExperiencePickup>();
        experiencePickup.experiencePoints = experiencePoints;
    }

    void DropMoney()
    {
        GameObject money = Instantiate(moneyPrefab, transform.position, Quaternion.identity);
        MoneyPickup moneyPickup = money.GetComponent<MoneyPickup>();
        moneyPickup.moneyAmount = moneyDrop;
    }

}
