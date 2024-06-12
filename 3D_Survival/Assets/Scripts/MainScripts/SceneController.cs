using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Main");
    }

    public void LoadTitleScene()
    {
        SceneManager.LoadScene("MonsterSurvivalTitle");
    }
}
