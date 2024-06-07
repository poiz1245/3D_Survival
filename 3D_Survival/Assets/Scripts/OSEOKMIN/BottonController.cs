using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BottonController : MonoBehaviour
{
    public void StartGoblinSurvival()
    {
        SceneManager.LoadScene("GoblinSurvival");
    }

    public void GoTitle()
    {
        SceneManager.LoadScene("GoblinSurvivalTitle");

    }
}
