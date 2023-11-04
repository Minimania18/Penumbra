using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int PlayerHealth { get; set; }

    public bool canAttack { get; private set; } = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        PlayerHealth = 100;
    }

    public void TakeDamage(int damageAmount)
    {
        GameManager.Instance.PlayerHealth -= damageAmount;
        Debug.Log(PlayerHealth);


        GameManager.Instance.PlayerHealth = Mathf.Max(0, GameManager.Instance.PlayerHealth);

        if (GameManager.Instance.PlayerHealth <= 0)
        {
            //???? don't know yet
        }
    }

    // Method to enable attacking
    public void EnableAttack(bool value)
    {
        canAttack = value;
    }
}