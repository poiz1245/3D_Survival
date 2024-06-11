using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMove : MonoBehaviour
{
    void Update()
    {
        Transform playerPosition = GameManager.Instance.player.transform;
        Vector3 moveToPlayer = new Vector3(playerPosition.position.x, transform.position.y, playerPosition.position.z);

        transform.position = moveToPlayer;
    }
}
