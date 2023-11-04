using UnityEngine;
using UnityEngine.EventSystems; 
public class ButtonHoverSound : MonoBehaviour, IPointerEnterHandler
{
    public AudioClip hoverSound;
    private AudioSource audioSource;
    

    void Start()
    {
        audioSource = FindObjectOfType<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (audioSource && hoverSound)
        {
            audioSource.PlayOneShot(hoverSound);
        }
    }
}