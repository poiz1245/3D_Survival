using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public int level = 1;
    public float hp;
    public float maxHp;
    public float playerAttackPower;
    public float playerShield;
    public float objectScanRadius;
    public float maxExperience = 100;
    public float currentExperience = 0;

    [SerializeField] float moveSpeed;
    [SerializeField] AudioSource levelUpSound;

    LayerMask dropObjectLayer;
    Rigidbody rigid;
    Animator anim;

    float horizontalInput;
    float verticalInput;

    public delegate void PlayerLevelChanged(int level);
    public event PlayerLevelChanged OnPlayerLevelChanged;

    public delegate void PlayerStateChanged(bool isDead);
    public event PlayerStateChanged OnPlayerStateChanged;
    public int playerLevel
    {
        get { return level; }
        set
        {
            if (level != value)
            {
                level = value;
                OnPlayerLevelChanged?.Invoke(level);
            }
        }
    }
    void DeadStateChanged(bool isDead)
    {
        OnPlayerStateChanged?.Invoke(isDead);
    }
    void Die(bool isDead)
    {
        gameObject.SetActive(false);
    }
    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        dropObjectLayer = LayerMask.GetMask("Exp");
        OnPlayerStateChanged += Die;
    }
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        AnimSet();
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
        print("플레이어 데미지 받음");

        float defence = playerShield * 0.1f;
        damage -= defence;

        if (hp > damage)
        {
            hp -= damage;
        }
        else if (hp <= damage)
        {
            hp -= damage;
            DeadStateChanged(true);
        }
    }
    public void AddExperience(int amount)
    {
        if(level >= 50)
        {
            return;
        }

        currentExperience += amount;
        if (currentExperience >= maxExperience)
        {
            LevelUp();
        }
    }
    void LevelUp()
    {
        levelUpSound.Play();
        playerLevel++;
        currentExperience = 0;
        maxExperience += 50;

        if (hp <= maxHp)
        {
            hp += maxHp * 0.1f;

            if (hp > maxHp)
            {
                hp = maxHp;
            }
        }
    }
}