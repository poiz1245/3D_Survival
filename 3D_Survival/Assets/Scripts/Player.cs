using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] LayerMask targetLayer;

    Rigidbody rigid;
    Animator anim;

    float horizontalInput;
    float verticalInput;

    public float hp;

    public float damage { get; private set; }
    public bool findTarget { get; private set; }
    public GameObject nearestTargetObject { get; private set; }
    public Transform nearestTargetPos { get; private set; }
    public float scanRadius { get; private set; }

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        scanRadius = 3f;
        findTarget = false;
        damage = 50f;
    }
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        AnimSet();

        if(hp <= 0)
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

        if (nearestTargetObject == null || !nearestTargetObject.activeSelf)
        {
            ScanTargets();
        }

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
    void ScanTargets()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, scanRadius, targetLayer);

        if (targets.Length > 0)
        {
            float closestDistance = Mathf.Infinity;
            Transform closestTarget = null;
            GameObject closestTargetObject = null;

            foreach (Collider target in targets)
            {
                float distance = Vector3.Distance(transform.position, target.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = target.transform;
                    closestTargetObject = target.gameObject;
                }
            }

            nearestTargetPos = closestTarget;
            nearestTargetObject = closestTargetObject;
            findTarget = true;
        }
        else
        {
            nearestTargetPos = null;
            findTarget = false;
        }
    }
    public void GetDamage(float damage)
    {
        hp -= damage;
    }
}