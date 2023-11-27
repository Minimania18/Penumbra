using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 6;
    private int currentHealth;

    // Reference to the HealthUI script
    public HealthUI healthUI;

    private void Start()
    {
        currentHealth = maxHealth;
        // Update the health UI at the start of the game
        healthUI.UpdateHealth(currentHealth);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.PageUp))
        {
            Heal(1);
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Max(currentHealth, 0);
        // Update the health UI
        healthUI.UpdateHealth(currentHealth);
    }

    public void Heal(int healAmount)
    {
        bool wasAtZero = currentHealth == 0;
        currentHealth += healAmount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        // Update the health UI
        if (wasAtZero && currentHealth > 0)
        {
            healthUI.SetFirstHeartToHalfIdle(); // Call this when healing from zero
        }
        else
        {
            healthUI.UpdateHealth(currentHealth);
        }
    }
}