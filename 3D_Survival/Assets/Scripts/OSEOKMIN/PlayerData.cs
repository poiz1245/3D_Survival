using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int level = 1;
    public int currentExperience = 0;
    public int maxExperience = 100;

    void Update()
    {
        // 경험치 오브젝트가 가까워지면 빨려들어오는 로직 추가 (여기서는 단순 거리 체크)
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5f);
        foreach (var hitCollider in hitColliders)
        {
            ExpObject goblinExp = hitCollider.GetComponent<ExpObject>();
            if (goblinExp != null)
            {
                //goblinExp.AttractToPlayer(transform);
            }
        }
    }

    public void AddExperience(int amount)
    {
        currentExperience += amount;
        if (currentExperience >= maxExperience)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        level++;
        currentExperience = 0;
        maxExperience += 50; // 레벨업 시 경험치 최대치 증가
        Debug.Log("Level Up! New Level: " + level);
    }

}
