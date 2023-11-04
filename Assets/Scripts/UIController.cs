using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text healthText;

    private void Update()
    {
        if (GameManager.Instance != null)
        {
            // Update text UI
            if (healthText != null)
                healthText.text = "Health: " + GameManager.Instance.PlayerHealth;

        }
    }
}