using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeMonster : MonoBehaviour
{
    public float hp;
    public float maxHp = 100f;
    public int damage;
    public float moveSpeed;
    public float attackRange;
    public int experienceAmount;
    public GameObject particlePrefab;

    [SerializeField] Transform expSpawnPoint;

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
        else if (!isDead && findPlayer)
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
            collider.enabled = false;
            rigid.isKinematic = true;
            Invoke("Die", 1.5f);
        }
    }
    void Die()
    {
        gameObject.SetActive(false);
    }
    void DropExp(bool monsterState)
    {
        GameObject exp = GameManager.Instance.dropObjectPool.GetDropObject(1);
        ExpObject expScript = exp.GetComponent<ExpObject>();

        if (expScript != null)
        {
            expScript.SetAmount(experienceAmount);
        }

        exp.transform.position = expSpawnPoint.position;
    }
    void AnimationSetting()
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
    void Rotate(Vector3 moveDir)
    {
        Quaternion deltaRotation = Quaternion.LookRotation(new Vector3(moveDir.x, rigid.velocity.y, moveDir.z));
        rigid.MoveRotation(deltaRotation);
    }
    void Move(Vector3 velocityChange)
    {
        rigid.AddForce(velocityChange, ForceMode.VelocityChange);
    }
    public void GetDamage(float damage)
    {
        GameObject myPrefabInstance = Instantiate(particlePrefab, transform.position, Quaternion.identity);
        ParticleSystem particleSystem = myPrefabInstance.GetComponent<ParticleSystem>();
        hp -= damage;
    }
    void ScanPlayer()
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
    private void Attack()
    {
        GameManager.Instance.player.GetDamage(damage);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.player.GetDamage(damage);
        }
    }
}
