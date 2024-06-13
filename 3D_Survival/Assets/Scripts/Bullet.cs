using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    float speed;
    float damage;
    float aliveTime;

    private void Update()
    {
        aliveTime += Time.deltaTime;
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if(aliveTime >1)
        {
            gameObject.SetActive(false);
            aliveTime = 0;
        }
    }

    public void SetStatus(float damage, float speed)
    {
        this.damage = damage;
        this.speed = speed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            MeleeMonster meleeMonster = other.gameObject.GetComponent<MeleeMonster>();
            RangedMonster monsterRanged = other.gameObject.GetComponent<RangedMonster>();
            BossMonster bossMonster = other.gameObject.GetComponent<BossMonster>();

            if (meleeMonster != null)
            {
                meleeMonster.GetDamage(damage);
            }
            if (monsterRanged != null)
            {
                monsterRanged.GetDamage(damage);
            }
            if (bossMonster != null)
            {
                bossMonster.GetDamage(damage);
            }

            gameObject.SetActive(false);
        }
    }
}