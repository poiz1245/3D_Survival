using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int level;
    public float speed;
    public float damage;
    public float range;

    public Weapon(int level, float speed, float damage, float range)
    {
        this.level = level;
        this.speed = speed;
        this.damage = damage;
        this.range = range;
    }
    public virtual void WeaponUpGrade()
    {
        level++;
    }
}
