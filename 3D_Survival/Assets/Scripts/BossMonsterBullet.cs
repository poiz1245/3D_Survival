using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterBullet : MonoBehaviour
{
    [SerializeField] float speed;
    float damage;

    float aliveTime;
    BossMonster bossMonster = new BossMonster();

    private void Update()
    {
        aliveTime += Time.deltaTime;
        //if (bossMonster.bulletSpawnComplate == true)
        //{
        // [S]총알 이동
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        //}

        if (aliveTime > 5)
        {
            gameObject.SetActive(false);
            aliveTime = 0;
        }
    }

    private void OnEnable()
    {
        //Vector3 moveDir = transform.position - GameManager.Instance.monsterPool.monsterPrefabs[2].transform.position;
        //Quaternion deltaRotation = Quaternion.LookRotation(new Vector3(0, 0, 0));
        //// 현재 객체를 목표 회전으로 회전
        //transform.rotation = deltaRotation;
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