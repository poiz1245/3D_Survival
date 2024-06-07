using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    public Sword() : base(0, 1f, 30f, 1f)
    {
    }
    void Update()
    {
        transform.Rotate(new Vector3(0, 1 * speed, 0));
    }

    public override void WeaponUpGrade()
    {
        base.WeaponUpGrade();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            Monster monster = other.GetComponent<Monster>();
            monster.GetDamage(damage);
        }
    }
}
