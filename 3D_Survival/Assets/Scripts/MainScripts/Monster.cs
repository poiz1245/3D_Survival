using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int experiencePoints = 10;
    public int moneyDrop = 10;
    public GameObject experiencePrefab;
    public GameObject moneyPrefab;

    Rigidbody rigid;
    Animator anim;
    CapsuleCollider collider;

    [SerializeField] float moveSpeed;
    [SerializeField] float maxHp = 100f;
    [SerializeField] float attackRange;
    [SerializeField] LayerMask playerLayer;

    float rotationSpeed = 100f;
    bool findPlayer = false;
    bool isDead = false;

    public float hp;

    private void Awake()
    {
        collider = GetComponent<CapsuleCollider>();
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        hp = maxHp;
        isDead = false;
        collider.enabled = true;
        rigid.isKinematic = false;
    }
    private void OnEnable()
    {
        hp = maxHp;
        isDead = false;
        collider.enabled = true;
        rigid.isKinematic = false;
    }

    void FixedUpdate()
    {
        Vector3 moveDir = GameManager.Instance.player.transform.position - transform.position;
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
        }
    }

    private void Update()
    {
        AnimationSetting();

        if (hp <= 0)
        {
            isDead = true;
            Invoke("Die", 1.5f);
        }
    }
    private void Die()
    {

        moneyDrop = 10;
        experiencePoints = 10;
        gameObject.SetActive(false); //오브젝트 풀링 사용중 이므로, 파괴가 필요한 오브젝트는 SetActive(false)로 비활성화
        collider.enabled = false;
        rigid.isKinematic = true;
        //DestroyObject(gameObject);
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

    public void ResetDieAnimation()
    {
        anim.SetBool("isDead", false);
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
}
