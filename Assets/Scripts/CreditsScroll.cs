using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreditsScroll : MonoBehaviour
{
    public float scrollSpeed = 20f;
    public TextMeshProUGUI creditsText;

    void Update()
    {
        if (creditsText != null)
        {
            creditsText.transform.position = new Vector2(creditsText.transform.position.x, creditsText.transform.position.y + scrollSpeed * Time.deltaTime);
        }
    }
}