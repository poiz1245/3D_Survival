using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterBullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float damage;

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
   /* private void OnCollisionEnter(Collision other)
    {
        print("콜라이더 접촉");
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.GetDamage(damage);
            }
            gameObject.SetActive(false);
        }
    }*/
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