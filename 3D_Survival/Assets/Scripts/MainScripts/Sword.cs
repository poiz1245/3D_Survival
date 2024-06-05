using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 0f;
    [SerializeField] float damage = 30f;

    void Update()
    {
        transform.position = GameManager.Instance.player.transform.position;
        transform.Rotate(new Vector3(0, 1 * rotationSpeed , 0));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            Monster monster = other.GetComponent<Monster>();
            monster.GetDamage(damage);
        }
    }
}
