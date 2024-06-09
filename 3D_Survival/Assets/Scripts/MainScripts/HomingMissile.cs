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
    HomingLauncher homingLaucher;
    //float time = 0; //베지어 곡선 만들 때 사용할 변수임

    private void Start()
    {
        //homingLaucher = WeaponManager.instance.weapons[2].GetComponent<HomingLauncher>();
        //maxDistance = WeaponManager.instance.weapons[2].range;
        //maxDistance = GameManager.Instance.player.monsterScanRadius;
    }

    private void OnEnable()
    {
        homingLaucher = WeaponManager.instance.weapons[2].GetComponent<HomingLauncher>();
        maxDistance = WeaponManager.instance.weapons[2].range;
        homingLaucher.OnNearestTargetChanged += TargetChange;
        //GameManager.Instance.player.OnNearestTargetChanged += TargetChange;
    }
    private void OnDisable()
    {
        homingLaucher.OnNearestTargetChanged -= TargetChange;
        //GameManager.Instance.player.OnNearestTargetChanged -= TargetChange;
    }
    void Update()
    {
        targetPos = homingLaucher.nearestTargetPos;
        //targetPos = GameManager.Instance.player.nearestTargetPos;

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
        print("aa");
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
