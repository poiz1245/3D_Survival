using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public Button startButton;

    private void Start()
    {
        // ��ũ��Ʈ�� ����Ǿ� �ִ� ��ư�� ���� Ŭ�� �̺�Ʈ ������ �߰�
        startButton.onClick.AddListener(OnStartButtonClick);
    }

    private void OnStartButtonClick()
    {
        // STAR ��ư�� Ŭ���Ǿ��� �� ȣ��Ǵ� ����
        Debug.Log("START ��ư�� Ŭ���Ǿ����ϴ�!");
        // ���⿡ �߰����� ������ ������ �� �ֽ��ϴ�.
        SceneManager.LoadScene("OSEOKMIN");
    }

}

