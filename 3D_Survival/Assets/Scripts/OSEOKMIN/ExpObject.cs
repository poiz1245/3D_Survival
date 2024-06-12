using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExpObject : MonoBehaviour
{
    public float attractSpeed;
   
    [SerializeField] GameObject light;
    //
    [SerializeField] AudioClip pickupSound; // ����� Ŭ���� �Ҵ��� �� �ִ� �ʵ�
    private AudioSource audioSource; // AudioSource ������Ʈ�� ������ ����
    //
    float rotationSpeed = 100f;
    int amount;
    Transform playerTransform;
    private void Awake()
    {
        playerTransform = GameManager.Instance.player.transform;
        //
        audioSource = GetComponent<AudioSource>(); // AudioSource ������Ʈ ��������
        //
    }
    private void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f, Space.World);
        //light.transform.Rotate(0f, -rotationSpeed * Time.deltaTime,0f );
    }
    public void SetAmount(int expAmount) //���� ���� �� ���Ͱ� ���� ����ġ ���� ������Ʈ�� �Ҵ�޴� �Լ�
    {
        amount = expAmount;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.player.AddExperience(amount);
            //
            PlayPickupSound(); // �Ҹ� ���
            //
            gameObject.SetActive(false);
        }
    }
    private void PlayPickupSound()
    {
        if (pickupSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(pickupSound);
        }
    }
    public void MoveToPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, attractSpeed * Time.deltaTime);
    }

}
