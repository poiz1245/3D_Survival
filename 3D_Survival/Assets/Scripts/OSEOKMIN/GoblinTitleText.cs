using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoblinTitleText : MonoBehaviour
{
    //public TextAlignment title;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI noticeText;

    // Start is called before the first frame updat

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Goblin");
    }
}
