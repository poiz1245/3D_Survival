using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExpObject : MonoBehaviour
{
    public float attractSpeed;
   
    [SerializeField] GameObject light;

    
    float rotationSpeed = 100f;
    int amount;
    Transform playerTransform;
    private void Awake()
    {
        playerTransform = GameManager.Instance.player.transform;

    }
    private void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f, Space.World);
        //light.transform.Rotate(0f, -rotationSpeed * Time.deltaTime,0f );
    }
    public void SetAmount(int expAmount) //몬스터 죽을 때 몬스터가 가진 경험치 값을 오브젝트에 할당받는 함수
    {
        amount = expAmount;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.player.AddExperience(amount);
            gameObject.SetActive(false);
        }
    }
    public void MoveToPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, attractSpeed * Time.deltaTime);
    }

}
