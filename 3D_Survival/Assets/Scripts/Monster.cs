using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : MonoBehaviour
{
    Rigidbody rigid;
    Animator anim;

    [SerializeField] float moveSpeed;
    [SerializeField] float rotationSpeed;

    float maxHp = 100;
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
            Invoke("Die", 3f);
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
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
        Quaternion deltaRotation = Quaternion.LookRotation(moveDir);
        rigid.MoveRotation(deltaRotation);
    }
    public void Move(Vector3 velocityChange)
    {
        rigid.AddForce(velocityChange, ForceMode.VelocityChange);
    }
    public void GetDamage(float damage)
    {
        print("데미지 받는중");
        hp -= damage;
    }
}
