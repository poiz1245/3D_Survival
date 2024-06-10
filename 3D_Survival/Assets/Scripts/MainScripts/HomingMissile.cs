using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class HomingMissile : MonoBehaviour
{

    float speed;
    float damage;

    Transform targetPos;
    float maxDistance;
    //float time = 0; //베지어 곡선 만들 때 사용할 변수임

    void TestMethod()
    {

    }
    private void OnEnable()
    {
        maxDistance = WeaponManager.instance.homingLauncher.range;
        WeaponManager.instance.homingLauncher.OnNearestTargetChanged += TargetChange;
    }
    private void OnDisable()
    {
        //WeaponManager.instance.homingLauncher.OnNearestTargetChanged -= TargetChange;
    }
    void Update()
    {
        targetPos = WeaponManager.instance.homingLauncher.nearestTargetPos;

        if (targetPos == null)
        {
            gameObject.SetActive(false);
            return;
        }

        Vector3 moveDir = targetPos.position - transform.position;
        transform.Translate(moveDir * speed * Time.deltaTime);
        /*if (time > 1)
        {
            time = 0;
        }
        float distance = Vector3.Distance(transform.position, targetPos.position);
        float offsetY = Mathf.Lerp(1f, 2f, distance / maxDistance);

        Vector3 middlePoint = (transform.position + targetPos.position) / 2;
        Vector3 thirdPoint = middlePoint + new Vector3(0, offsetY, 0);

        Vector3 p1 = Vector3.Lerp(transform.position, thirdPoint, time);
        Vector3 p2 = Vector3.Lerp(thirdPoint, targetPos.position, time);

        transform.position = Vector3.Lerp(p1, p2, time / 2);
        time += Time.deltaTime;*///베지어 곡선
    }
    void TargetChange(Monster target)
    {
        gameObject.SetActive(false);
    }
    public void SetStatus(float damage, float speed)
    {
        this.damage = damage;
        this.speed = speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.gameObject.CompareTag("Monster"))
        {
            Monster monster = other.gameObject.GetComponent<Monster>();
            monster.GetDamage(damage);
            gameObject.SetActive(false);
        }
    }
}
