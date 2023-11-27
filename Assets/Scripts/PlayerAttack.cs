using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    public Collider2D attackHitbox;
    public int attackDamage = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
        attackHitbox.enabled = false; 
    }

    void Update()
    {
        if (GameManager.Instance != null && Input.GetButtonDown("Fire1") && GameManager.Instance.canAttack)
        {
            Attack();
        }
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");

    }


    public void ActivateHitbox()
    {
        attackHitbox.enabled = true;
    }


    public void DeactivateHitbox()
    {
        attackHitbox.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(attackDamage);
            }
        }
    }
}