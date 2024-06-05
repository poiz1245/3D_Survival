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

    [SerializeField] float moveSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float maxHp = 100;

    bool isDead = false;

    public float hp { get; private set; }


    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Vector3 moveDir = GameManager.Instance.player.transform.position - transform.position;

        Vector3 targetVelocity = new Vector3(moveDir.x * moveSpeed, rigid.velocity.y, moveDir.z * moveSpeed);
        Vector3 velocityChange = (targetVelocity - rigid.velocity);

        if (!isDead)
        {
            Move(velocityChange);
            Rotate(moveDir * rotationSpeed);
        }
        else
        {
            rigid.velocity = Vector3.zero;
        }
    }

    private void OnEnable()
    {
        hp = maxHp;
        isDead = false;
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
        //DestroyObject(gameObject);
    }

    private void AnimationSetting()
    {
        if (hp <= 0)
        {
            anim.SetBool("isDead", true);

        }
        else
        {
            anim.SetBool("isDead", false);
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
}
