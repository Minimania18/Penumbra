using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isFacingRight = true;

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

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float movey = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

        if (moveX > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (moveX < 0 && isFacingRight)
        {
            Flip();
        }

   
        if (Mathf.Abs(moveX) > 0.0001f)  
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
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

    }
}