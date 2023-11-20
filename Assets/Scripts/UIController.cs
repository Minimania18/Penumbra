using UnityEngine;
using UnityEngine.SceneManagement;
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Load the main menu scene
            // Replace "MainMenuScene" with the name of your main menu scene
            SceneManager.LoadScene("Menu");
        }
    }
}