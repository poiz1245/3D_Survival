using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JoySticInput : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public Rigidbody rigid;
    public Joystick joystick; // ����Ƽ UI���� Joystick ������Ʈ�� ����

    void Update()
    {
#if UNITY_IOS || UNITY_ANDROID
        HandleJoystickInput();
#endif
    }

    void HandleJoystickInput()
    {
        // ���̽�ƽ �Է� ���� �޾ƿ�
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        // y�� �̵��� �����ϱ� ���� y ���� 0���� ����
        Vector3 moveDir = new Vector3(horizontalInput, 0, verticalInput);

        // �÷��̾��� �̵� �� ȸ�� ó��
        if (moveDir.magnitude >= 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            rigid.MoveRotation(Quaternion.Slerp(rigid.rotation, targetRotation, Time.deltaTime * moveSpeed));

            Vector3 targetVelocity = moveDir.normalized * moveSpeed;
            Vector3 velocityChange = targetVelocity - new Vector3(rigid.velocity.x, 0, rigid.velocity.z);
            rigid.AddForce(velocityChange, ForceMode.VelocityChange);
        }
    }
}
