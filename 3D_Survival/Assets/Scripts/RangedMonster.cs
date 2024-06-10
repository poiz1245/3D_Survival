using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedMonster : MonoBehaviour
{
    public int experiencePoints = 0;       
    public int moneyDrop = 0;            
    public float hp;

    [SerializeField] float moveSpeed;
    [SerializeField] float maxHp = 100f;
    [SerializeField] float attackRange;
    [SerializeField] int experienceAmount; // �⺻ ����ġ ��
    [SerializeField] int moneyAmount;
    [SerializeField] float damage;

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
        AnimationSetting();

        if (hp <= 0)
        {
            hp = 0;
            monsterState = true;
            Invoke("Die", 1.5f);
        }
    }
    private void Die()
    {
        moneyDrop = 10;
        experiencePoints = 10;
        gameObject.SetActive(false); //������Ʈ Ǯ�� ����� �̹Ƿ�, �ı��� �ʿ��� ������Ʈ�� SetActive(false)�� ��Ȱ��ȭ
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
    private void AnimationSetting()
    {
        if (hp <= 0)
        {
            anim.SetBool("isDead", true);
            anim.SetBool("isAttack", false);
        }
        else if (findPlayer)
        {
            anim.SetBool("isAttack", true);
        }
        else
        {
            anim.SetBool("isDead", false);
            anim.SetBool("isAttack", false);
        }
    }
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
    public void MonsterBulletSpawn(int index)
    {
        MonsterBullet monsterBullet = GameManager.Instance.bulletPool.GetBullet(index).GetComponent<MonsterBullet>();
        monsterBullet.SetDamage(damage);
        monsterBullet.transform.position = transform.position;
        monsterBullet.transform.rotation = transform.rotation;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.player.GetDamage(damage);
        }
    }
}