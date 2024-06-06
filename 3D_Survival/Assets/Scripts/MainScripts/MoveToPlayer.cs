using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayer : MonoBehaviour
{
    void Update()
    {
        transform.position = GameManager.Instance.player.transform.position;
    }
}
