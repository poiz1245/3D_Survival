using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoE : MonoBehaviour
{
    [SerializeField] LayerMask targetLayer;
    [SerializeField] GameObject particle;

    public float aoeRadius = 0f;
    public float damage = 0f;
    public float coolTime = 0f;

    private void Start()
    {
        InvokeRepeating("Attack", 0f, coolTime);
    }
    public void UpgradeSkill(float upgradePercent)
    {
        float increasedAmount = upgradePercent / 100;
        Vector3 particleScale = particle.transform.localScale;
        aoeRadius += aoeRadius * increasedAmount;
        particle.transform.localScale += particleScale * increasedAmount;
    }
    void Attack()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, aoeRadius, targetLayer);

        foreach (Collider target in targets)
        {
            Monster monsterScript = target.GetComponent<Monster>();
            monsterScript.GetDamage(damage);
        }
    }
    /*    private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, aoeRadius);
        }*/
}
