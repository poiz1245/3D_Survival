using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartAction : MonoBehaviour
{
    public void GoToNextScene()
    {
        // ���� ������ �̵�
        SceneManager.LoadScene("GoblinSurvival");
    }



}
