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
//        // 업그레이드 적용
//        ApplyUpgrade(upgradeType);

//        // 버튼들 제거
//        foreach (Transform child in transform.parent)
//        {
//            Destroy(child.gameObject);
//        }

//        // 게임 재개
//        Time.timeScale = 1f;
//    }

//    void ApplyUpgrade(string upgradeType)
//    {
//        PlayerController player = FindObjectOfType<PlayerController>();

//        switch (upgradeType)
//        {
//            case "Max HP 증가":
//                player.MaxHP += 10;
//                break;
//            case "공격력 증가":
//                player.AttackPower += 5;
//                break;
//            case "방어력 증가":
//                player.Defense += 3;
//                break;
//            case "공격 범위 증가":
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
        // buttonText 필드가 할당되지 않은 경우, 자식 객체에서 TextMeshProUGUI 컴포넌트를 찾아 할당
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
        // 업그레이드 적용
        ApplyUpgrade(upgradeType);

        // 버튼들 제거
        foreach (Transform child in transform.parent)
        {
            Destroy(child.gameObject);
        }

        // 게임 재개
        Time.timeScale = 1f;
    }

    void ApplyUpgrade(string upgradeType)
    {
        PlayerController player = FindObjectOfType<PlayerController>();

        switch (upgradeType)
        {
            case "Max HP 증가":
                player.MaxHP += 10;
                break;
            case "공격력 증가":
                player.AttackPower += 5;
                break;
            case "방어력 증가":
                player.Defense += 3;
                break;
            case "공격 범위 증가":
                player.AttackRange += 1f;
                break;
        }
    }
}

//        }
//    }
//}
