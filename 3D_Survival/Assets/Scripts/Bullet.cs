using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float damage;

    float aliveTime;
    private void Update()
    {
        aliveTime += Time.deltaTime;
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if(aliveTime >5)
        {
            gameObject.SetActive(false);
            aliveTime = 0;
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            Monster monster = other.gameObject.GetComponent<Monster>();
            if (monster != null)
            {
                monster.GetDamage(damage);
            }
            RangedMonster monsterRanged = other.gameObject.GetComponent<RangedMonster>();
            if (monsterRanged != null)
            {
                monsterRanged.GetDamage(damage);
            }
            gameObject.SetActive(false);
        }
    }
}