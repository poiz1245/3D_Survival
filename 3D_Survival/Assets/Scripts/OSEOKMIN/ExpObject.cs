using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ExpObject : MonoBehaviour
{
    public float attractSpeed;
   
    [SerializeField] GameObject light;
    //
    //[SerializeField] AudioClip pickupSound; // ����� Ŭ���� �Ҵ��� �� �ִ� �ʵ�
    [SerializeField] AudioSource audioSource; // AudioSource ������Ʈ�� ������ ����
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
    public void SetAmount(int expAmount) //���� ���� �� ���Ͱ� ���� ����ġ ���� ������Ʈ�� �Ҵ�޴� �Լ�
    {
        amount = expAmount;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.Play(); //�Ҹ� �� ��� �ȵǴ��� ���κҸ�
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
