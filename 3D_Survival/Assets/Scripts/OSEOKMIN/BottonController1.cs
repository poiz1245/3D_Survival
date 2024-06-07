using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BottonController1 : MonoBehaviour
{
    public Button starButton;

    private void Start()
    {
        // ��ũ��Ʈ�� ����Ǿ� �ִ� ��ư�� ���� Ŭ�� �̺�Ʈ ������ �߰�
        starButton.onClick.AddListener(OnStarButtonClick);
    }

    private void OnStarButtonClick()
    {
        // STAR ��ư�� Ŭ���Ǿ��� �� ȣ��Ǵ� ����
        Debug.Log("STAR ��ư�� Ŭ���Ǿ����ϴ�!");
        // ���� ������ �̵�
        SceneManager.LoadScene("���� ���� �̸�");
    }
}
