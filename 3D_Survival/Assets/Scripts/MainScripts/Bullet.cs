using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Bullet : MonoBehaviour
{
    Transform targetPos;
    float time = 0;
    float maxDistance;

    private void Start()
    {
        maxDistance = GameManager.Instance.player.scanRadius;
    }
    void Update()
    {
        if (time > 1)
        {
            time = 0;
        }

        targetPos = GameManager.Instance.player.nearestTargetPos;

        float distance = Vector3.Distance(transform.position, targetPos.position);
        float offsetY = Mathf.Lerp(1f, 2f, distance / maxDistance);

        Vector3 middlePoint = (transform.position + targetPos.position) / 2;
        Vector3 thirdPoint = middlePoint + new Vector3(0, offsetY, 0);

        Vector3 p1 = Vector3.Lerp(transform.position, thirdPoint, time);
        Vector3 p2 = Vector3.Lerp(thirdPoint, targetPos.position, time);

        transform.position = Vector3.Lerp(p1, p2, time);
        time += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            Monster monster = other.gameObject.GetComponent<Monster>();
            monster.GetDamage(GameManager.Instance.player.damage / 2);
            gameObject.SetActive(false);

            if (monster.hp <=0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
