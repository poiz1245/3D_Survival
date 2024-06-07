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
        // ����ġ ������Ʈ�� ��������� ���������� ���� �߰� (���⼭�� �ܼ� �Ÿ� üũ)
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5f);
        foreach (var hitCollider in hitColliders)
        {
            GoblinExperience goblinExp = hitCollider.GetComponent<GoblinExperience>();
            if (goblinExp != null)
            {
                goblinExp.AttractToPlayer(transform);
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
        maxExperience += 50; // ������ �� ����ġ �ִ�ġ ����
        Debug.Log("Level Up! New Level: " + level);
    }

}
