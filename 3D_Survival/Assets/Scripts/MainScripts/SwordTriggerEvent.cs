using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordTriggerEvent : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            Sword sword = GetComponentInParent<Sword>();
            sword.OnTriggerEnter(other);
        }
    }
}
