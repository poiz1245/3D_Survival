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
            // 체력이 0이하인 경우 처리
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
        print("1페이즈");
        int count = 25;
        float radius = 3f; // 원의 반지름 설정
        float intervalAngle = 360f / count;
        float weightAngle = 0;


        while (true)
        {
            for (int i = 0; i < count; ++i)
            {
                // 각도에 따라 원 위에 위치 계산
                float angle = weightAngle + intervalAngle * i;
                float x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
                float y = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;

                // 총알 생성 및 위치 설정
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
        print("2페이즈");
        int count = 25;
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
        print("3페이즈");
        int count = 25;
        float radius = 3f;
        float intervalAngle = 360f / count;
        float weightAngle = 0;

        attackRange += 10;      // [S]3페이즈 들어오면 공격범위 10 늘어나게

        while (true)
        {
            Vector3 playerPosition = GameManager.Instance.player.transform.position;
            for (int i = 0; i < count; ++i)
            {
                // 각도에 따라 원 위에 위치 계산
                float angle = weightAngle + intervalAngle * i;
                float x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
                float y = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;

                // 총알 생성 및 위치 설정
                Vector3 spawnPosition = transform.position + new Vector3(x, 0f, y);

                Vector3 controlPoint = (spawnPosition + playerPosition) * 1.5f;     //베지어곡선 제어점 

                // 총알의 방향을 플레이어 위치를 향하도록 설정
                Vector3 direction = (playerPosition - spawnPosition).normalized;
                direction.y = 0;
                Quaternion rotation = Quaternion.LookRotation(direction);

                GameObject clone = GameManager.Instance.bulletPool.GetBullet(3);
                clone.transform.position = spawnPosition;
                clone.transform.rotation = rotation;
                clone.transform.Rotate(0f, 180f, 0f);

                // 베지어 곡선을 사용하여 총알 이동
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
        print("4페이즈");
        int count = 25; // 총알 개수
        float radius = 3f; // 원의 반지름
        float intervalAngle = 360f / count; // 총알 사이의 각도 간격
        float weightAngle = 0; // 각도의 가중치
        float maxDistance = 10f; // 플레이어와의 최대 거리

        attackRange += 10; // 3페이즈 들어오면 공격범위 10 늘어나게

        while (true)
        {
            Vector3 playerPosition = GameManager.Instance.player.transform.position;

            for (int i = 0; i < count; ++i)
            {
                // 무작위한 방향 벡터 생성
                Vector3 randomDirection = UnityEngine.Random.insideUnitSphere;

                // 최대 거리 내에서 무작위 위치 생성
                Vector3 randomPosition = transform.position + randomDirection * radius;
                Vector3 spawnPosition = transform.position + (randomPosition - transform.position).normalized * Mathf.Min(maxDistance, randomPosition.magnitude);
                spawnPosition.y = 0;

                // 총알의 방향을 플레이어 쪽으로 설정
                Vector3 direction = (playerPosition - spawnPosition).normalized;
                //Quaternion rotation = Quaternion.LookRotation(direction);
                Quaternion rotation = Quaternion.Euler(0, playerPosition.y, 0);

                // 총알 생성 및 위치, 회전 설정
                GameObject clone = GameManager.Instance.bulletPool.GetBullet(3);
                clone.transform.position = spawnPosition;
                clone.transform.rotation = rotation;
                //clone.transform.Rotate(0f, 180f, 0f);

                // 총알 이동 코루틴 호출
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
        float duration = 1f; // 베지어 곡선을 따라 이동하는 데 걸리는 시간

        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            // 베지어 곡선 계산
            float t = timeElapsed / duration;
            Vector3 position = CalculateBezierPoint(t, startPoint, controlPoint, endPoint);
            position.y = 0;

            // 총알 이동
            bulletTransform.position = position;

            // 시간 업데이트
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        // 총알 이동 완료 후 파괴
        //Destroy(bulletTransform.gameObject);
    }

    Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        // 베지어 곡선 포인트 계산
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
        float speed = 10f; // 총알의 이동 속도
        direction.y = 0;

        while (true)
        {
            // 총알을 이동 방향으로 이동
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
