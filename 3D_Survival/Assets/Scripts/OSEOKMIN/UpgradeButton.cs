//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;


//public class UpgradeButton : MonoBehaviour
//{
//    public TextMeshProUGUI buttonText;
//    private string upgradeType;

//    public void SetUpgradeType(string type)
//    {
//        upgradeType = type;
//        buttonText.text = upgradeType;
//    }

//    public void OnClick()
//    {
//        // ���׷��̵� ����
//        ApplyUpgrade(upgradeType);

//        // ��ư�� ����
//        foreach (Transform child in transform.parent)
//        {
//            Destroy(child.gameObject);
//        }

//        // ���� �簳
//        Time.timeScale = 1f;
//    }

//    void ApplyUpgrade(string upgradeType)
//    {
//        PlayerController player = FindObjectOfType<PlayerController>();

//        switch (upgradeType)
//        {
//            case "Max HP ����":
//                player.MaxHP += 10;
//                break;
//            case "���ݷ� ����":
//                player.AttackPower += 5;
//                break;
//            case "���� ����":
//                player.Defense += 3;
//                break;
//            case "���� ���� ����":
//                player.AttackRange += 1f;
//                break;


using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public TextMeshProUGUI buttonText;
    private string upgradeType;

    private void Awake()
    {
        // buttonText �ʵ尡 �Ҵ���� ���� ���, �ڽ� ��ü���� TextMeshProUGUI ������Ʈ�� ã�� �Ҵ�
        if (buttonText == null)
        {
            buttonText = GetComponentInChildren<TextMeshProUGUI>();
        }

        if (buttonText == null)
        {
            Debug.LogError("buttonText is not assigned.");
        }
    }

    public void SetUpgradeType(string type)
    {
        if (buttonText != null)
        {
            upgradeType = type;
            buttonText.text = upgradeType;
        }
        else
        {
            Debug.LogError("buttonText is not assigned.");
        }
    }

    public void OnClick()
    {
        // ���׷��̵� ����
        ApplyUpgrade(upgradeType);

        // ��ư�� ����
        foreach (Transform child in transform.parent)
        {
            Destroy(child.gameObject);
        }

        // ���� �簳
        Time.timeScale = 1f;
    }

    void ApplyUpgrade(string upgradeType)
    {
        PlayerController player = FindObjectOfType<PlayerController>();

        switch (upgradeType)
        {
            case "Max HP ����":
                player.MaxHP += 10;
                break;
            case "���ݷ� ����":
                player.AttackPower += 5;
                break;
            case "���� ����":
                player.Defense += 3;
                break;
            case "���� ���� ����":
                player.AttackRange += 1f;
                break;
        }
    }
}

//        }
//    }
//}
