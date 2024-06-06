using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayer : MonoBehaviour
{
    [SerializeField] Transform playerPos;
    
    void Update()
    {
        transform.position = playerPos.position;
    }
}