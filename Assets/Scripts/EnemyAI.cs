using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform[] patrolPoints; // Points between which the enemy will patrol
    public float patrolSpeed = 2f; // how fast they will patrol
    public float chaseSpeed = 5f; //how fast they will chase
    public float chaseDistance = 5f; //distance the enemy will start chasing the player
    public float stopChaseDistance = 10f; // Distance at which the enemy stops chasing the player

    private Transform playerTransform;
    private int currentPatrolPointIndex = 0;
    private bool isChasing = false;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
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
    }

    void Patrol()
    {
        Transform patrolPoint = patrolPoints[currentPatrolPointIndex];
        if (Vector3.Distance(transform.position, patrolPoint.position) < 0.1f)
        {
            currentPatrolPointIndex = (currentPatrolPointIndex + 1) % patrolPoints.Length;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, patrolPoint.position, patrolSpeed * Time.deltaTime);
        }
    }

    void ChasePlayer()
    {
        Vector3 currentPosition = transform.position;

        Vector3 targetPosition = new Vector3(playerTransform.position.x, currentPosition.y, playerTransform.position.z);

        transform.position = Vector3.MoveTowards(currentPosition, targetPosition, chaseSpeed * Time.deltaTime);
    }
}