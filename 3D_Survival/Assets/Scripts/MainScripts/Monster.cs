using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float hp;
    public float maxHp = 100f;
    public int damage;
    public float moveSpeed;
    public float attackRange;
    public int experienceAmount; // 기본 경험치 양
    public GameObject particlePrefab;

    public Monster(float hp, float maxHp, int damage, float moveSpeed,  float attackRange, int experienceAmount)
    {
        this.hp = hp;
        this.maxHp = maxHp;
        this.moveSpeed = moveSpeed;
        this.damage = damage;
        this.attackRange = attackRange;
        this.experienceAmount = experienceAmount;
    }
}
