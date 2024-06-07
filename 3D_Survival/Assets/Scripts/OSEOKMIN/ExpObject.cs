using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExpObject : MonoBehaviour
{
    public float attractSpeed = 5f;

    [SerializeField] float scanRadious;

    int amount;
    Transform playerTransform;
    LayerMask playerLayer;
    private void Awake()
    {
        playerTransform = GameManager.Instance.player.transform;
        playerLayer = LayerMask.GetMask("Player");
    }
    void Update()
    {
        ScanPlayer();
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
            gameObject.SetActive(false);
        }
    }

    void ScanPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, scanRadious, playerLayer);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider != null)

            {
                transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, attractSpeed * Time.deltaTime);
            }
        }
    }
    /*    public void AttractToPlayer(Transform player)
        {
            playerTransform = player;
            attracting = true;
        }
    */
}
