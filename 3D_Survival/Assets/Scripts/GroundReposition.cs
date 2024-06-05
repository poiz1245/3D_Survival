using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundReposition : MonoBehaviour
{
    [SerializeField] Transform playerPosition;
    [SerializeField] float groundMoveDis;

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Area"))
        {
            return;
        }

        float diffX = Mathf.Abs(transform.position.x - playerPosition.position.x);
        float diffZ = Mathf.Abs(transform.position.z - playerPosition.position.z);

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        float playerXDir = horizontalInput;
        float playerZDir = verticalInput;

        if (Mathf.Abs(diffX - diffZ) <= 0.3f)
        {
            transform.Translate(playerXDir * groundMoveDis, 0, playerZDir * groundMoveDis);
        }
        else if (diffX > diffZ)
        {
            transform.Translate(Vector3.right * playerXDir * groundMoveDis);
        }
        else if (diffX < diffZ)
        {
            transform.Translate(Vector3.forward * playerZDir * groundMoveDis);
        }
        
    }
}
