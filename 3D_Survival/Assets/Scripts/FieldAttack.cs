using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldAttack : MonoBehaviour
{
    [SerializeField] LayerMask targetLayer;
    public float aoeRadius = 0f;
    public float aoeDamage = 0f;
    public float coolTime = 0f;

    private Dictionary<Monster, Coroutine> attackedMonster = null;
    private void Awake()
    {
        attackedMonster = new Dictionary<Monster, Coroutine>();
    }
    // Update is called once per frame
    private void Update()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, aoeRadius, targetLayer);

        // ������ �����ߴ� ���� �߿��� ����� ���͸� üũ�ϱ� ���� ���
        List<Monster> monstersToRemove = new List<Monster>(attackedMonster.Keys);

        foreach (Collider target in targets)
        {
            Monster monsterScript = target.GetComponent<Monster>();
            if (!attackedMonster.ContainsKey(monsterScript))
            {
                Coroutine coroutine = StartCoroutine(SetDamage(monsterScript));
                attackedMonster.Add(monsterScript, coroutine);
            }

            // ���Ͱ� �����Ǿ����Ƿ� ���� ��Ͽ��� �ش� ���� ����
            monstersToRemove.Remove(monsterScript);
        }

        // ���� ��Ͽ� �ִ� ���͵鿡 ���� �ڷ�ƾ�� �����ϰ� Dictionary���� ����
        foreach (Monster monsterToRemove in monstersToRemove)
        {
            StopCoroutine(attackedMonster[monsterToRemove]);
            attackedMonster.Remove(monsterToRemove);
        }
    }

    IEnumerator SetDamage(Monster monsterScript)
    {
        while (true)
        {
            monsterScript.GetDamage(aoeDamage);
            yield return new WaitForSeconds(coolTime);
        }
    }
}
