using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject buttonPanel;
    public List<GameObject> button;
    public Transform[] spots;

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
        List<int> getIndicesToRemove = GetIndicesToRemove();

        for (int i = getIndicesToRemove.Count - 1; i >= 0; i--)
        {
            button.RemoveAt(getIndicesToRemove[i]);
        }

        buttonPanel.SetActive(true);

        List<int> randomNumbers = GetRandomNumber();
        for (int i = 0; i < 3; i++)
        {
            button[randomNumbers[i]].transform.position = spots[i].position;
            button[randomNumbers[i]].SetActive(true);
        }

        Time.timeScale = 0;
    }
    List<int> GetIndicesToRemove()
    {
        List<int> indicesToRemove = new();

        for(int i = 0; i < button.Count; i++) //Count 8
        {
            button[i].SetActive(false);

            if (i < WeaponManager.instance.weaponsList.Count) //Length 4
            {
                if (WeaponManager.instance.weaponsList[i].level == 4)
                {
                    indicesToRemove.Add(i);
                    WeaponManager.instance.weaponsList.RemoveAt(i); //한번 조건에 걸려서 제거된 무기는 다시 걸리지 않도록 삭제
                }
            }
        }

        return indicesToRemove;
    }
    List<int> GetRandomNumber()
    {
        List<int> numbers = new();
        List<int> result = new();

        for (int i = 0; i < button.Count; i++)
        {
            numbers.Add(i);
        }

        for (int i = 0; i < 3; ++i)
        {
            int index = Random.Range(0, numbers.Count);
            result.Add(numbers[index]);
            numbers.RemoveAt(index);
        }

        return result;
    }
}