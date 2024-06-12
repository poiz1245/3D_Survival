using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadGameScene()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Main");
    }

    public void LoadTitleScene()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MonsterSurvivalTitle");
    }
}
