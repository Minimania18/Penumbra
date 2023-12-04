using UnityEngine;

public class Jumping : MonoBehaviour
{
    private PlayerMovement playerMovement;

    private Rigidbody2D rb;
    private bool jumpRequested;
    private float jumpRequestTime;
    private float jumpBufferTime = 0.2f; // Time window for buffering the jump

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float jumpVelocity = 10f;
    public float maxFallSpeed = -10f;
    public AudioSource audioSource;
    public AudioClip jumpSound;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumpRequested = true;
            jumpRequestTime = Time.time;
        }
        const float epsilon = 0.01f;
        if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < epsilon)
        {
            // Set the initial jump velocity
            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            audioSource.PlayOneShot(jumpSound);
        }

        if (rb.velocity.y < 0 || (rb.velocity.y > 0 && !Input.GetButton("Jump")))
        {
            // Apply an additional multiplier as we approach the apex of the jump
            float multiplier = (rb.velocity.y < 0) ? fallMultiplier : lowJumpMultiplier;
            if (Mathf.Abs(rb.velocity.y) < epsilon)
            {
                // Apply even more gravity force to minimize the pause at the top of the jump
                multiplier *= 3; 
            }
            rb.velocity += Vector2.up * Physics2D.gravity.y * (multiplier - 1) * Time.deltaTime;
        }
        // Clamp the fall speed to not exceed the maximum fall speed
        if (rb.velocity.y < maxFallSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, maxFallSpeed);
        }
    }
    void FixedUpdate()
    {
        // Check for buffered jump
        if (jumpRequested && Time.time - jumpRequestTime <= jumpBufferTime && playerMovement.isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
            jumpRequested = false; // Reset the jump request
        }
    }
}