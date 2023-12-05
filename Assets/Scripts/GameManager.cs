using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int PlayerHealth { get; set; }
    public GameObject NextSpawnPoint { get; private set; }
    public bool canAttack { get; private set; } = false;

    public Image fadeImage;
    public float fadeDuration = 1f;

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
       //PlayerHealth = 100;
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
        StartCoroutine(FadeAndLoadScene(sceneName, spawnPointName));
    }

    private IEnumerator FadeAndLoadScene(string sceneName, string spawnPointName)
    {
        // Fade out
        yield return StartCoroutine(Fade(1));

        // Set spawn point and load new scene
        PlayerPrefs.SetString("NextSpawnPoint", spawnPointName);
        Debug.Log("Transitioning to scene: " + sceneName + " with spawn point: " + spawnPointName);
        SceneManager.LoadScene(sceneName);

        // Fade in
        //yield return StartCoroutine(Fade(0));
    }

    private IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = fadeImage.color.a;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            fadeImage.color = new Color(0f, 0f, 0f, newAlpha);
            yield return null;
        }
    }
    /* public void ResetSpawnPointToDefault()
     {
         // Set the default spawn point name
         PlayerPrefs.SetString("NextSpawnPoint", "DefaultSpawnPointName");
     }
    */
}