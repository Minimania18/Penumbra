using UnityEngine;

public class Jumping : MonoBehaviour
{
    private Rigidbody2D rb;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float jumpVelocity = 10f;  // Adjust this to set the initial jump velocity
    public float maxFallSpeed = -10f;  // Maximum fall speed

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        const float epsilon = 0.01f;
        if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < epsilon)
        {
            // Set the initial jump velocity
            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
        }

        if (rb.velocity.y < 0 || (rb.velocity.y > 0 && !Input.GetButton("Jump")))
        {
            // Apply an additional multiplier as we approach the apex of the jump
            float multiplier = (rb.velocity.y < 0) ? fallMultiplier : lowJumpMultiplier;
            if (Mathf.Abs(rb.velocity.y) < epsilon)
            {
                // Apply even more gravity force to minimize the pause at the top of the jump
                multiplier *= 3;  // Adjust this to fine-tune the jump
            }
            rb.velocity += Vector2.up * Physics2D.gravity.y * (multiplier - 1) * Time.deltaTime;
        }

        // Clamp the fall speed to not exceed the maximum fall speed
        if (rb.velocity.y < maxFallSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, maxFallSpeed);
        }
    }
}