using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fillImage; // The foreground image of the health bar

    public void SetHealth(float fillAmount)
    {
        fillAmount = Mathf.Clamp01(fillAmount); // Clamp between 0 and 1

        fillImage.fillAmount = fillAmount;

        // Change color from green (1, 1, 0) to red (1, 0, 0)
        fillImage.color = Color.Lerp(Color.red, Color.green, fillAmount);
    }
}