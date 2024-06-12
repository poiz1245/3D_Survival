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
    public GameObject respawnArea;
    private void Start()
    {
        playerLayer = LayerMask.GetMask("Player");
    }

    private void Update()
    {
        ScanPlayer();
        if (!findPlayer)
        {
            MonsterReporition();
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
        if (!findPlayer)
        {
            Vector3 respawnPos = respawnArea.transform.position;
            print(respawnPos);
            transform.position = respawnPos;
            findPlayer = true;
        }
    }
}
