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


    [SerializeField] float moveSpeed;
    public float hp { get; private set; }

    float maxHp = 100;
    Rigidbody rigid;
    Animator anim;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Vector3 moveDir = GameManager.Instance.player.transform.position - transform.position;

        Vector3 targetVelocity = moveDir * moveSpeed;
        Vector3 velocityChange = (targetVelocity - rigid.velocity);

        Move(velocityChange);
        Rotate(moveDir);
    }

    private void OnEnable()
    {
        hp = maxHp;
    }
    private void Update()
    {
        AnimationSetting();

        if (hp <= 0)
        {
            Invoke("Die", 3f);
        }
    }

  

    private void Die()
    {

        moneyDrop = 10;
        experiencePoints = 10;
        DestroyObject(gameObject);
        //gameObject.SetActive(false);
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
        hp -= damage;
    }
}
