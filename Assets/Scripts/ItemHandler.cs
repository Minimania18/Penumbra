using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    public float floatSpeed = 1f; 
    public float floatHeight = 0.5f; 
    public AudioClip pickupSound; 

    private Vector3 startPosition;
    private float floatTimer;
    private AudioSource audioSource; 

    protected virtual void Start()
    {
        startPosition = transform.position;
        audioSource = GetComponent<AudioSource>(); 
    }

    protected virtual void Update()
    {
     
        floatTimer += Time.deltaTime * floatSpeed;
        float newY = startPosition.y + Mathf.Sin(floatTimer) * floatHeight;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ItemPickedUp(other.gameObject);
        }
    }

    protected virtual void ItemPickedUp(GameObject player)
    {
    
        if (pickupSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(pickupSound);

     
            Collider2D collider = GetComponent<Collider2D>();
            if (collider != null) collider.enabled = false;

            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            if (renderer != null) renderer.enabled = false;

            Destroy(gameObject, pickupSound.length);
        }
        else
        {
            Destroy(gameObject);
        }

    }
}
