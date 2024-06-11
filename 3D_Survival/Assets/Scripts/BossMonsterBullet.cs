using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterBullet : MonoBehaviour
{
    [SerializeField] float speed;
    float damage;

    float aliveTime;

    private void Update()
    {
        aliveTime += Time.deltaTime;
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (aliveTime > 5)
        {
            gameObject.SetActive(false);
            aliveTime = 0;
        }
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.GetDamage(damage);
            }
            gameObject.SetActive(false);
        }
    }

}