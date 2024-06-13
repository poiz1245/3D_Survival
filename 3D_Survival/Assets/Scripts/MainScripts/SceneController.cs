using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [SerializeField] Button button;

    private void Start()
    {
        button.interactable = true;
    }
    public void LoadGameScene()
    {
        Time.timeScale = 1.0f;
        button.interactable = false;
        StartCoroutine(LoadSceneWithDelay("Main", 1.5f)); // 2ÃÊ Áö¿¬
    }
    private IEnumerator LoadSceneWithDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
    public void LoadTitleScene()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MonsterSurvivalTitle");
    }
}
