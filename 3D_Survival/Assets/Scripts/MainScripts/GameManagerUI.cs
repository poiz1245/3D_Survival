using UnityEngine;
using UnityEngine.UI;

public class GameManagerUI : MonoBehaviour
{
    public void ResumeGame()
    {
        Time.timeScale = 1f; // 게임 재개

        // 버튼들 제거
        foreach (Transform child in GameManager.Instance.buttonContainer)
        {
            Destroy(child.gameObject);
        }

    }
}