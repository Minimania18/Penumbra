using UnityEngine;

public class SceneTransitionTrigger : MonoBehaviour
{
    public string sceneToLoad;
    public string spawnPointNameInNewScene;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.TransitionToScene(sceneToLoad, spawnPointNameInNewScene);
        }
    }
}