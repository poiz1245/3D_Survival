using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartAction : MonoBehaviour
{
    public void GoToNextScene()
    {
        // 다음 씬으로 이동
        SceneManager.LoadScene("GoblinSurvival");
    }



}
