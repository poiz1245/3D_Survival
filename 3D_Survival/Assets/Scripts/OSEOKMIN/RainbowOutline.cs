using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RainbowOutline : MonoBehaviour
{
    private Outline outline;
    private Color[] rainbowColors = {
        Color.red, Color.yellow, Color.green, Color.cyan, Color.blue, Color.magenta
    };

    void Awake()
    {
        outline = GetComponent<Outline>();
        if (outline == null)
        {
            outline = gameObject.AddComponent<Outline>();
        }
        outline.effectDistance = new Vector2(10 , 10);
        StartCoroutine(ChangeOutlineColor());
    }

    IEnumerator ChangeOutlineColor()
    {
        int index = 0;
        while (true)
        {
            Color startColor = rainbowColors[index];
            Color endColor = rainbowColors[(index + 1) % rainbowColors.Length];
            float duration = 1.0f; // Change duration to your preference
            float time = 0;

            while (time < duration)
            {
                outline.effectColor = Color.Lerp(startColor, endColor, time / duration);
                time += Time.deltaTime;
                yield return null;
            }

            index = (index + 1) % rainbowColors.Length;
        }
    }
}
