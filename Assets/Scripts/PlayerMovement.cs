using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isFacingRight = true;
    private bool wasInAir = false;

    public LayerMask groundLayer;
    public bool isGrounded;
    private int framesSinceGrounded;
    public Transform groundCheckPoint;
    public float groundCheckRadius = 0.2f;
    private const int FALL_DELAY_FRAMES = 4; // Number of frames to delay the fall check
    public AudioSource runningSound;
    public AudioSource audioSource;
    public AudioClip landingSound;
    //private float footstepDelay = 0.5f; // Delay between footsteps in seconds
    //private float footstepTimer = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Application.targetFrameRate = 60;

        string spawnPointName = PlayerPrefs.GetString("NextSpawnPoint", "DefaultSpawnPointName");
        GameObject spawnPoint = GameObject.Find(spawnPointName);
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.transform.position;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
            Debug.Log("Player is grounded");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
            Debug.Log("Player is NOT grounded");

        }
    }

    private void Update()
    {
        isGrounded = IsGrounded();
        float moveX = Input.GetAxis("Horizontal");
        float movey = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);


        if (!isGrounded)
        {
            framesSinceGrounded++;
        }
        else
        {
            framesSinceGrounded = 0;
        }

        if (moveX > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (moveX < 0 && isFacingRight)
        {
            Flip();
        }


        if (Mathf.Abs(moveX) > 0.0001f && isGrounded)
        {
            animator.SetBool("isRunning", true);
            /*
            if (!runningSound.isPlaying)  // Check if the sound is not already playing
            {
                runningSound.Play();  // Play the running sound
            }
            */
        }
        else
        {
            animator.SetBool("isRunning", false);
            /*
            if (runningSound.isPlaying)
            {
                runningSound.Stop();  // Stop the running sound
            }
            */
        }
        if (!isGrounded)
        {
            wasInAir = true;
        }

        if (isGrounded && wasInAir)
        {
            audioSource.PlayOneShot(landingSound);
            wasInAir = false;
        }


        void Flip()
        {
            isFacingRight = !isFacingRight; 

            Vector3 playerScale = transform.localScale;
            playerScale.x *= -1; 
            transform.localScale = playerScale;
        }
        //loads menu if esc is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //ResetSpawnPointToDefault();
            SceneManager.LoadScene("Menu");
        }
        // Trigger Jump Animation
        if (IsJumping())
        {
            animator.SetBool("isJumping", true);
            animator.SetBool("isFalling", false);
        }
        // Trigger Fall Animation
        else if (IsFalling())
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", true);
        }
        else
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
        }
    }
    private bool IsJumping()
    {
        return rb.velocity.y > 0.3f; // 0.1f is a threshold to avoid floating point imprecision
    }

    private bool IsFalling()
    {
        return !isGrounded && framesSinceGrounded > FALL_DELAY_FRAMES; //rb.velocity.y < -0.3f
    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);
    }

}