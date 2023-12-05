using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform[] patrolPoints; // Points between which the enemy will patrol
    public float patrolSpeed = 2f; // how fast they will patrol
    public float patrolPauseDuration = 2f;
    public float chaseSpeed = 5f; //how fast they will chase
    public float chaseDistance = 5f; //distance the enemy will start chasing the player
    public float stopChaseDistance = 10f; // Distance at which the enemy stops chasing the player
    public float attackDistance = 1f; // Distance within which the enemy can attack
    public float attackCooldown = 2f; // Cooldown time between attacks
    private float lastAttackTime; // Time when the last attack occurred

    public Animator animator;
    private Transform playerTransform;
    private int currentPatrolPointIndex = 0;
    private Vector3 lastPosition;
    private bool isChasing = false;
    private float patrolPauseTimer;
    private bool isWaiting;
    private Vector3 targetPosition;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        lastPosition = transform.position;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (isChasing)
        {
            if (distanceToPlayer > stopChaseDistance)
            {
                isChasing = false;
            }
            else
            {
                ChasePlayer();
            }
        }
        else
        {
            if (distanceToPlayer <= chaseDistance)
            {
                isChasing = true;
            }
            else
            {
                Patrol();
            }
        }
        if (isChasing)
        {
            targetPosition = playerTransform.position;
        }
        else
        {
            targetPosition = patrolPoints[currentPatrolPointIndex].position;
        }

        FlipSprite();
    }

    void Patrol()
    {
        if (isWaiting)
        {
            if (patrolPauseTimer > 0)
            {
                patrolPauseTimer -= Time.deltaTime;
            }
            else
            {
                isWaiting = false;
                currentPatrolPointIndex = (currentPatrolPointIndex + 1) % patrolPoints.Length;
            }
        }
        else
        {
            Transform patrolPoint = patrolPoints[currentPatrolPointIndex];
            transform.position = Vector3.MoveTowards(transform.position, patrolPoint.position, patrolSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, patrolPoint.position) < 0.1f)
            {
                isWaiting = true;
                patrolPauseTimer = patrolPauseDuration;
            }
        }
    }

    void ChasePlayer()
    {
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = new Vector3(playerTransform.position.x, currentPosition.y, playerTransform.position.z);
        float distanceToPlayer = Vector3.Distance(currentPosition, playerTransform.position);

        // Check if within attack range and cooldown has passed
        if (distanceToPlayer <= attackDistance && Time.time > lastAttackTime + attackCooldown)
        {
            AttackPlayer();
            lastAttackTime = Time.time; // Update the last attack time
        }
        else
        {
            transform.position = Vector3.MoveTowards(currentPosition, targetPosition, chaseSpeed * Time.deltaTime);
        }
    }
    void AttackPlayer()
    {
        animator.SetTrigger("Attack");
    }
 void FlipSprite()
{
    // Determine the direction of movement
    bool shouldFaceRight = transform.position.x > targetPosition.x;  // Inverted check

    // Check if the sprite's facing direction matches the movement direction
    bool isFacingRight = transform.localScale.x > 0;
    
    if (shouldFaceRight != isFacingRight)
    {
        // Flip the sprite by negating its x scale
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}
}