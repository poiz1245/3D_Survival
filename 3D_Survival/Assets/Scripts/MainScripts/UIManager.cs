using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject buttonPanel;
    public GameObject[] button;
    public Transform[] spots;

    List<int> randomNumbers;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }
    private void Start()
    {
        GameManager.Instance.player.OnPlayerLevelChanged += PlayerLevelUp;
    }
    private void PlayerLevelUp(int level)
    {
        randomNumbers = GetRandomNumber(0, button.Length, 3);

        buttonPanel.SetActive(true);

        for (int i = 0; i < 3; i++)
        {
            button[randomNumbers[i]].transform.position = spots[i].position;
            button[randomNumbers[i]].SetActive(true);
        }

        Time.timeScale = 0;
    }
    List<int> GetRandomNumber(int min, int max, int count)
    {
        List<int> numbers = new List<int>();
        List<int> result = new List<int>();

        for (int i = min; i < max; i++)
        {
            numbers.Add(i);
            button[i].SetActive(false);
        }

        for (int i = 0; i < count; ++i)
        {
            int index = Random.Range(0, numbers.Count);
            result.Add(numbers[index]);
            numbers.RemoveAt(index);
        }

        return result;
    }
}