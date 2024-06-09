using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public float monsterScanRadius;
    public float objectScanRadius;
    public float hp;
    public float maxHp;
    public int level = 1;
    public int maxExperience = 100;
    public float playerAttackPower;
    public float playerShield;

    [SerializeField] float moveSpeed;
    [SerializeField] int currentExperience = 0;

    LayerMask targetLayer;
    LayerMask dropObjectLayer;
    Rigidbody rigid;
    Animator anim;

    float horizontalInput;
    float verticalInput;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        targetLayer = LayerMask.GetMask("Monster");
        dropObjectLayer = LayerMask.GetMask("Exp");
    }
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        AnimSet();

        if (hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    private void FixedUpdate()
    {
        Vector3 moveDir = new Vector3(horizontalInput, rigid.velocity.y, verticalInput);

        Vector3 targetVelocity = moveDir.normalized * moveSpeed;
        Vector3 velocityChange = (targetVelocity - rigid.velocity);

        Movement(velocityChange);
        Rotation(moveDir);
        ScanDropObject();

    }
    private void AnimSet()
    {
        if (horizontalInput != 0f || verticalInput != 0f)
        {
            anim.SetBool("isRun", true);
        }
        else
        {
            anim.SetBool("isRun", false);
        }
    }
    private void Rotation(Vector3 moveDir)
    {
        if (moveDir.x != 0 || moveDir.z != 0)
        {
            Quaternion deltaRotation = Quaternion.LookRotation(moveDir.normalized);
            rigid.MoveRotation(deltaRotation);
        }
    }
    private void Movement(Vector3 velocityChange)
    {
        rigid.AddForce(velocityChange, ForceMode.VelocityChange);
    }

    void ScanDropObject()
    {
        Collider[] targetObject = Physics.OverlapSphere(transform.position, objectScanRadius, dropObjectLayer);

        if (targetObject.Length > 0)
        {
            foreach (Collider target in targetObject)
            {
                ExpObject expObject = target.GetComponent<ExpObject>();
                expObject.MoveToPlayer();
            }
        }
    }
    public void GetDamage(float damage)
    {
        hp -= damage;
    }
    public void AddExperience(int amount)
    {
        currentExperience += amount;
        if (currentExperience >= maxExperience)
        {
            LevelUp();
        }
    }
    void LevelUp()
    {
        level++;
        currentExperience = 0;
        maxExperience += 50;
    }
}