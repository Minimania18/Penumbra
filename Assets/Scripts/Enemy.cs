using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject bloodEffect;
    public int health = 100;

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (bloodEffect != null)
        {
            Debug.Log("Enemy died, instantiating blood effect.");
            Instantiate(bloodEffect, transform.position, Quaternion.identity);
            if (bloodEffect != null)
            {
                GameObject bloodEffectInstance = Instantiate(bloodEffect, transform.position, Quaternion.identity);
                ParticleSystem particleSystem = bloodEffectInstance.GetComponent<ParticleSystem>();
                if (particleSystem != null)
                {
                    particleSystem.Play();
                }
            }
            if (health <= 0)
            {
                Die();
            }
        }
    }
    private void Die()
    {

        Destroy(gameObject);
    }
}