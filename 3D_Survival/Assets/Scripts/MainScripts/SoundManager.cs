using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] AudioSource buttonClickSound;
    //[SerializeField] AudioSource deadSound;
    //[SerializeField] AudioSource winSound;
    [SerializeField] AudioSource monsterDeadSound;
    [SerializeField] Button[] buttons;
    [SerializeField] GameObject themeMusic;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //GameManager.Instance.player.OnPlayerStateChanged += GameOver;
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].onClick.AddListener(SoundPlay);
        }
    }
    public void GameClear()
    {
        themeMusic.SetActive(false);
    }
    /*void GameOver(bool gameOver)
    {
        deadSound.Play();
    }
    public void GameWin()
    {
        winSound.Play();
    }*/
    void SoundPlay()
    {
        buttonClickSound.Play();
    }


}
