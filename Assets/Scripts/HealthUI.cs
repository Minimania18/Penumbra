using UnityEngine;

public class HealthUI : MonoBehaviour
{
    public Animator[] heartAnimators;
    public Sprite halfHeartSprite;
    public void UpdateHealth(int currentHealth)
    {
        for (int i = 0; i < heartAnimators.Length; i++)
        {
            if (i < currentHealth / 2)
                heartAnimators[i].Play("FullHeart");
            else if (i * 2 < currentHealth)
                heartAnimators[i].SetTrigger("ToHalf");
            else
                heartAnimators[i].SetTrigger("ToEmpty");
        }
    }
    public void SetFirstHeartToHalfIdle()
    {
        int currentHeart = 0;
        if (heartAnimators.Length > currentHeart)
        {
            heartAnimators[currentHeart].Play("HalfIdle"); // Play the HalfIdle animation for the first heart
        }
    }
}