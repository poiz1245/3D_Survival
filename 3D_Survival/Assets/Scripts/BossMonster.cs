using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.Mathematics;
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

    public bool bulletSpawnComplate = false;


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
        Attack();
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
    void Attack()
    {
        float perHp = (hp / maxHp) * 100;
        print(perHp);

        if (hp <= 0)
        {
            // ü���� 0������ ��� ó��
            return;
        }

        if (perHp >= 70f)
        {
            StartCoroutine(BossStage1Bullet());
        }
        else if (perHp >= 50f)
        {
            StartCoroutine(BossStage2Bullet());
        }
        else if ((perHp >= 30f))
        {
            StartCoroutine(BossStage3Bullet());
        }
        else
        {
            StartCoroutine(BossStage4Bullet());
        }

    }
    IEnumerator BossStage1Bullet()
    {
        print("1������");
        int count = 25;
        float radius = 3f; // ���� ������ ����
        float intervalAngle = 360f / count;
        float weightAngle = 0;


        while (true)
        {
            for (int i = 0; i < count; ++i)
            {
                // ������ ���� �� ���� ��ġ ���
                float angle = weightAngle + intervalAngle * i;
                float x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
                float y = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;

                // �Ѿ� ���� �� ��ġ ����
                Vector3 spawnPosition = transform.position + new Vector3(x, 0f, y);
                Vector3 direction = (transform.position - spawnPosition).normalized;
                Quaternion rotation = Quaternion.LookRotation(direction);


                GameObject clone = GameManager.Instance.bulletPool.GetBullet(3);
                clone.transform.position = spawnPosition;
                clone.transform.rotation = rotation;
                clone.transform.Rotate(0f, 180f, 0f);

            }
            bulletSpawnComplate = true;

            yield return new WaitForSeconds(1f);

            weightAngle += 1;

            yield return new WaitForSeconds(3);
            bulletSpawnComplate = false;
        }
    }

    IEnumerator BossStage2Bullet()
    {
        print("2������");
        int count = 25;
        float radius = 3f; // ���� ������ ����
        float intervalAngle = 360f / count;
        float weightAngle = 0;


        while (true)
        {
            for (int i = 0; i < count; ++i)
            {
                yield return new WaitForSeconds(0.1f);
                // ������ ���� �� ���� ��ġ ���
                float angle = weightAngle + intervalAngle * i;
                float x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
                float y = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;

                // �Ѿ� ���� �� ��ġ ����
                Vector3 spawnPosition = transform.position + new Vector3(x, 0f, y);
                Vector3 direction = (transform.position - spawnPosition).normalized;
                Quaternion rotation = Quaternion.LookRotation(direction);


                GameObject clone = GameManager.Instance.bulletPool.GetBullet(3);
                clone.transform.position = spawnPosition;
                clone.transform.rotation = rotation;
                clone.transform.Rotate(0f, 180f, 0f);

            }
            bulletSpawnComplate = true;

            //yield return new WaitForSeconds(1f);

            weightAngle += 1;

            //yield return new WaitForSeconds(3);
            bulletSpawnComplate = false;
        }
    }

    IEnumerator BossStage3Bullet()
    {
        print("3������");
        int count = 25;
        float radius = 3f;
        float intervalAngle = 360f / count;
        float weightAngle = 0;

        attackRange += 10;      // [S]3������ ������ ���ݹ��� 10 �þ��

        while (true)
        {
            Vector3 playerPosition = GameManager.Instance.player.transform.position;
            for (int i = 0; i < count; ++i)
            {
                // ������ ���� �� ���� ��ġ ���
                float angle = weightAngle + intervalAngle * i;
                float x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
                float y = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;

                // �Ѿ� ���� �� ��ġ ����
                Vector3 spawnPosition = transform.position + new Vector3(x, 0f, y);

                Vector3 controlPoint = (spawnPosition + playerPosition) * 1.5f;     //������ ������ 

                // �Ѿ��� ������ �÷��̾� ��ġ�� ���ϵ��� ����
                Vector3 direction = (playerPosition - spawnPosition).normalized;
                direction.y = 0;
                Quaternion rotation = Quaternion.LookRotation(direction);

                GameObject clone = GameManager.Instance.bulletPool.GetBullet(3);
                clone.transform.position = spawnPosition;
                clone.transform.rotation = rotation;
                clone.transform.Rotate(0f, 180f, 0f);

                // ������ ��� ����Ͽ� �Ѿ� �̵�
                StartCoroutine(MoveBulletBezier(clone.transform, spawnPosition, playerPosition, controlPoint));
            }
            bulletSpawnComplate = true;

            yield return new WaitForSeconds(1f);

            weightAngle += 1;

            yield return new WaitForSeconds(3);
            bulletSpawnComplate = false;
        }
    }

    IEnumerator BossStage4Bullet()
    {
        print("4������");
        int count = 25; // �Ѿ� ����
        float radius = 3f; // ���� ������
        float intervalAngle = 360f / count; // �Ѿ� ������ ���� ����
        float weightAngle = 0; // ������ ����ġ
        float maxDistance = 10f; // �÷��̾���� �ִ� �Ÿ�

        attackRange += 10; // 3������ ������ ���ݹ��� 10 �þ��

        while (true)
        {
            Vector3 playerPosition = GameManager.Instance.player.transform.position;

            for (int i = 0; i < count; ++i)
            {
                // �������� ���� ���� ����
                Vector3 randomDirection = UnityEngine.Random.insideUnitSphere;

                // �ִ� �Ÿ� ������ ������ ��ġ ����
                Vector3 randomPosition = transform.position + randomDirection * radius;
                Vector3 spawnPosition = transform.position + (randomPosition - transform.position).normalized * Mathf.Min(maxDistance, randomPosition.magnitude);
                spawnPosition.y = 0;

                // �Ѿ��� ������ �÷��̾� ������ ����
                Vector3 direction = (playerPosition - spawnPosition).normalized;
                //Quaternion rotation = Quaternion.LookRotation(direction);
                Quaternion rotation = Quaternion.Euler(0, playerPosition.y, 0);

                // �Ѿ� ���� �� ��ġ, ȸ�� ����
                GameObject clone = GameManager.Instance.bulletPool.GetBullet(3);
                clone.transform.position = spawnPosition;
                clone.transform.rotation = rotation;
                //clone.transform.Rotate(0f, 180f, 0f);

                // �Ѿ� �̵� �ڷ�ƾ ȣ��
                StartCoroutine(MoveBulletStraight(clone.transform, direction));
            }

            bulletSpawnComplate = true;

            yield return new WaitForSeconds(1f);

            weightAngle += 1;

            yield return new WaitForSeconds(3);
            bulletSpawnComplate = false;
        }
    }


    IEnumerator MoveBulletBezier(Transform bulletTransform, Vector3 startPoint, Vector3 endPoint, Vector3 controlPoint)
    {
        float duration = 1f; // ������ ��� ���� �̵��ϴ� �� �ɸ��� �ð�

        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            // ������ � ���
            float t = timeElapsed / duration;
            Vector3 position = CalculateBezierPoint(t, startPoint, controlPoint, endPoint);
            position.y = 0;

            // �Ѿ� �̵�
            bulletTransform.position = position;

            // �ð� ������Ʈ
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        // �Ѿ� �̵� �Ϸ� �� �ı�
        //Destroy(bulletTransform.gameObject);
    }

    Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        // ������ � ����Ʈ ���
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = uu * p0;
        p += 2 * u * t * p1;
        p += tt * p2;
        return p;
    }

    IEnumerator MoveBulletStraight(Transform bulletTransform, Vector3 direction)
    {
        float speed = 10f; // �Ѿ��� �̵� �ӵ�
        direction.y = 0;

        while (true)
        {
            // �Ѿ��� �̵� �������� �̵�
            bulletTransform.position += direction * speed * Time.deltaTime;

            yield return null;
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
