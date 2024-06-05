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

        // 기존에 감지했던 몬스터 중에서 사라진 몬스터를 체크하기 위한 목록
        List<Monster> monstersToRemove = new List<Monster>(attackedMonster.Keys);

        foreach (Collider target in targets)
        {
            Monster monsterScript = target.GetComponent<Monster>();
            if (!attackedMonster.ContainsKey(monsterScript))
            {
                Coroutine coroutine = StartCoroutine(SetDamage(monsterScript));
                attackedMonster.Add(monsterScript, coroutine);
            }

            // 몬스터가 감지되었으므로 제거 목록에서 해당 몬스터 제거
            monstersToRemove.Remove(monsterScript);
        }

        // 제거 목록에 있는 몬스터들에 대해 코루틴을 중지하고 Dictionary에서 제거
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
