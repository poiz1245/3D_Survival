using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JoySticInput : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public Rigidbody rigid;
    public Joystick joystick; // 유니티 UI에서 Joystick 오브젝트를 연결

    void Update()
    {
#if UNITY_IOS || UNITY_ANDROID
        HandleJoystickInput();
#endif
    }

    void HandleJoystickInput()
    {
        // 조이스틱 입력 값을 받아옴
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        // y축 이동을 방지하기 위해 y 값을 0으로 설정
        Vector3 moveDir = new Vector3(horizontalInput, 0, verticalInput);

        // 플레이어의 이동 및 회전 처리
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
