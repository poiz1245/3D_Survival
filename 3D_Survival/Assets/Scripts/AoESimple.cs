using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoESimple : MonoBehaviour
{
    [SerializeField] LayerMask targetLayer;
    public float aoeRadius = 0f;
    public float damage = 0f;
    public float coolTime = 0f;

    private void Start()
    {
        InvokeRepeating("Attack", 0f, coolTime);
    }

    // 공격 실행
    void Attack()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, aoeRadius, targetLayer);

        foreach (Collider target in targets)
        {
            Monster monsterScript = target.GetComponent<Monster>();
            monsterScript.GetDamage(damage);
            Debug.Log("공격");
        }
    }
}
