using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class BossMonster : MonoBehaviour
{
    public float hp;
    public float maxHp = 100f;
    public int damage;
    public float moveSpeed;
    public float attackRange;
    public int experienceAmount;

    Rigidbody rigid;
    Animator anim;
    new CapsuleCollider collider;

    LayerMask playerLayer;
    float rotationSpeed = 100f;
    bool findPlayer = false;
    bool isDead = false;


    public delegate void MonsterStateChange(bool isDead);
    public event MonsterStateChange OnMonsterStateChanged;
    public bool monsterState
    {
        get { return isDead; }
        set
        {
            if (isDead != value)
            {
                isDead = value;
                OnMonsterStateChanged?.Invoke(isDead);
            }
        }
    }
    private void Awake()
    {
        collider = GetComponent<CapsuleCollider>();
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        hp = maxHp;
        monsterState = false;
        collider.enabled = true;
        rigid.isKinematic = false;
    }
    private void Start()
    {
        playerLayer = LayerMask.GetMask("Player");
        StartCoroutine(CreatBullet());
    }
    private void OnEnable()
    {
        hp = maxHp;
        monsterState = false;
        collider.enabled = true;
        rigid.isKinematic = false;

        OnMonsterStateChanged += DropExp;
    }
    private void OnDisable()
    {
        OnMonsterStateChanged -= DropExp;
    }
    void FixedUpdate()
    {
        Vector3 moveDir = (GameManager.Instance.player.transform.position - transform.position).normalized;
        Vector3 targetVelocity = new Vector3(moveDir.x * moveSpeed, rigid.velocity.y, moveDir.z * moveSpeed);
        Vector3 velocityChange = (targetVelocity - rigid.velocity);

        ScanPlayer();

        if (!isDead && !findPlayer)
        {
            Move(velocityChange);
            Rotate(moveDir * rotationSpeed);
        }
        else
        {
            rigid.velocity = Vector3.zero;
            Rotate(moveDir * rotationSpeed);
        }
    }
    private void Update()
    {
        //AnimationSetting();

        if (hp <= 0)
        {
            hp = 0;
            monsterState = true;
            Invoke("Die", 1.5f);
        }
    }
    private void Die()
    {
        gameObject.SetActive(false);
        collider.enabled = false;
        rigid.isKinematic = true;
    }
    void DropExp(bool monsterState)
    {
        GameObject exp = GameManager.Instance.dropObjectPool.GetDropObject(1);
        ExpObject expScript = exp.GetComponent<ExpObject>();

        if (expScript != null)
        {
            expScript.SetAmount(experienceAmount);
        }

        exp.transform.position = transform.position;
    }
    //private void AnimationSetting()
    //{
    //    if (hp <= 0)
    //    {
    //        anim.SetBool("isDead", true);
    //        anim.SetBool("isAttack", false);
    //    }
    //    else if (findPlayer)
    //    {
    //        anim.SetBool("isAttack", true);
    //    }
    //    else
    //    {
    //        anim.SetBool("isDead", false);
    //        anim.SetBool("isAttack", false);
    //    }
    //}
    public void Rotate(Vector3 moveDir)
    {
        Quaternion deltaRotation = Quaternion.LookRotation(new Vector3(moveDir.x, rigid.velocity.y, moveDir.z));
        rigid.MoveRotation(deltaRotation);
    }
    public void Move(Vector3 velocityChange)
    {
        rigid.AddForce(velocityChange, ForceMode.VelocityChange);
    }
    public void GetDamage(float damage)
    {
        hp -= damage;
    }
    public void ScanPlayer()
    {
        Collider[] target = Physics.OverlapSphere(transform.position, attackRange, playerLayer);
        if (target.Length > 0)
        {
            findPlayer = true;
        }
        else
        {
            findPlayer = false;
        }
    }
    //public void Attack(int index)
    //{
    //    BossMonsterBullet bossMonsterBullet = GameManager.Instance.bulletPool.GetBullet(3).GetComponent<BossMonsterBullet>();
    //    //monsterBullet.SetDamage(damage);
    //    bossMonsterBullet.transform.position = transform.position;
    //    bossMonsterBullet.transform.rotation = transform.rotation;

    //}

    IEnumerator CreatBullet()
    {
        int count = 15;
        float radius = 3f; // 원의 반지름 설정
        float intervalAngle = 360f / count;
        float weightAngle = 0;

        while (true)
        {
            for (int i = 0; i < count; ++i)
            {
                yield return new WaitForSeconds(0.1f);
                // 각도에 따라 원 위에 위치 계산
                float angle = weightAngle + intervalAngle * i;
                float x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
                float y = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;

                // 총알 생성 및 위치 설정
                Vector3 spawnPosition = transform.position + new Vector3(x, 0f, y);
               // GameObject clone = Instantiate(GameManager.Instance.bulletPool.GetBullet(3), spawnPosition, Quaternion.identity);
                GameObject clone = GameManager.Instance.bulletPool.GetBullet(3);
                clone.transform.position = spawnPosition;

                clone.transform.RotateAround(Vector3.up, i*intervalAngle);

            }

            yield return new WaitForSeconds(1f);

            weightAngle += 1;

            yield return new WaitForSeconds(3);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.player.GetDamage(damage);
        }
    }
}
