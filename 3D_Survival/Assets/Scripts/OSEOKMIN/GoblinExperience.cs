using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GoblinExperience : MonoBehaviour
{
    public float attractSpeed = 5f;
    private int amount;
    private Transform playerTransform;
    private bool attracting;

    void Update()
    {
        if (attracting && playerTransform != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, attractSpeed * Time.deltaTime);
        }
    }

    public void SetAmount(int expAmount)
    {
        amount = expAmount;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerData playerData = other.GetComponent<PlayerData>();
            if (playerData != null)
            {
                playerData.AddExperience(amount);
                gameObject.SetActive(false); // 경험치 오브젝트 비활성화
            }
        }
    }

    public void AttractToPlayer(Transform player)
    {
        playerTransform = player;
        attracting = true;
    }

}
