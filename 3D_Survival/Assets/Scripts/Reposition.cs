using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    public float repositionRange;
    LayerMask playerLayer;
    bool findPlayer = false;
    private Transform playerTransform;

    private void Start()
    {
        playerLayer = LayerMask.GetMask("Player");
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null )
        {
            playerTransform = playerObject.transform;
        }
    }

    private void Update()
    {
        ScanPlayer();

        if (gameObject.activeSelf)
        {
            if (!findPlayer)
            {
                MonsterReporition();
            }
        }
    }

    public void ScanPlayer()
    {
        Collider[] target = Physics.OverlapSphere(transform.position, repositionRange, playerLayer);
        if (target.Length > 0)
        {
            findPlayer = true;
        }
        else
        {
            findPlayer = false;
        }
    }

    public void MonsterReporition()
    {
        if (playerTransform != null) 
        {
            Vector3 respawnPos = playerTransform.Find("respawnPos").transform.position;
            transform.position = respawnPos;
            findPlayer = true;
        }
    }
}