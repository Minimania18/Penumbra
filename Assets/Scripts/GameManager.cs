using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int PlayerHealth { get; set; }
    public GameObject NextSpawnPoint { get; private set; }

    public bool canAttack { get; private set; } = false;

    private void Awake()
    {
       //ResetSpawnPointToDefault();
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
    public void TransitionToScene(string sceneName, string spawnPointName)
    {
        PlayerPrefs.SetString("NextSpawnPoint", spawnPointName);
        Debug.Log("Transitioning to scene: " + sceneName + " with spawn point: " + spawnPointName);
        SceneManager.LoadScene(sceneName);
    }
   /* public void ResetSpawnPointToDefault()
    {
        // Set the default spawn point name
        PlayerPrefs.SetString("NextSpawnPoint", "DefaultSpawnPointName");
    }
   */
}