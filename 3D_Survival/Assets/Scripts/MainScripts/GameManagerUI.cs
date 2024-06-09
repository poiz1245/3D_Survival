using UnityEngine;
using UnityEngine.UI;

public class GameManagerUI : MonoBehaviour
{
    public void ResumeGame()
    {
        Time.timeScale = 1f; // ���� �簳

        // ��ư�� ����
        foreach (Transform child in GameManager.Instance.buttonContainer)
        {
            Destroy(child.gameObject);
        }

    }
}