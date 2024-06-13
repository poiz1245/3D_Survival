using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ExpObject : MonoBehaviour
{
    public float attractSpeed;
   
    [SerializeField] GameObject light;
    //
    //[SerializeField] AudioClip pickupSound; // 오디오 클립을 할당할 수 있는 필드
    [SerializeField] AudioSource audioSource; // AudioSource 컴포넌트를 저장할 변수
    //
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
    }
    public void SetAmount(int expAmount) //몬스터 죽을 때 몬스터가 가진 경험치 값을 오브젝트에 할당받는 함수
    {
        amount = expAmount;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.Play(); //소리 왜 재생 안되는지 원인불명
            GameManager.Instance.player.AddExperience(amount);
            InVisibleObject(0.5f);
        }
    }
   IEnumerator InVisibleObjectWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
    void InVisibleObject(float delay)
    {
        StartCoroutine(InVisibleObjectWithDelay(delay));
    }
    public void MoveToPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, attractSpeed * Time.deltaTime);
    }

}
