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
        // 스크립트가 연결되어 있는 버튼에 대한 클릭 이벤트 리스너 추가
        starButton.onClick.AddListener(OnStarButtonClick);
    }

    private void OnStarButtonClick()
    {
        // STAR 버튼이 클릭되었을 때 호출되는 동작
        Debug.Log("STAR 버튼이 클릭되었습니다!");
        // 게임 씬으로 이동
        SceneManager.LoadScene("게임 씬의 이름");
    }
}
