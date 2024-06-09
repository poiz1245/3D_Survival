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
        // 스크립트가 연결되어 있는 버튼에 대한 클릭 이벤트 리스너 추가
        startButton.onClick.AddListener(OnStartButtonClick);
    }

    private void OnStartButtonClick()
    {
        // STAR 버튼이 클릭되었을 때 호출되는 동작
        Debug.Log("START 버튼이 클릭되었습니다!");
        // 여기에 추가적인 동작을 구현할 수 있습니다.
        SceneManager.LoadScene("OSEOKMIN");
    }

}

