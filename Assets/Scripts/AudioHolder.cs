using UnityEngine;

public class AudioHolder : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip footstepSound;

    public void PlayFootstep()
    {
        if (footstepSound != null)
        {
            audioSource.PlayOneShot(footstepSound);
        }
    }
}