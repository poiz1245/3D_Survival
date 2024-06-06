using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Missile : MonoBehaviour
{
    public float speed;
    [SerializeField] float damage;

    private void Update()
    {
        transform.Translate(Vector3.forward * speed);
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
            gameObject.SetActive(false);
        }
    }
}