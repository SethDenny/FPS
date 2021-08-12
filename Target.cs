using System.Collections;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;
    public GameObject destroyedVersion;
    public bool destructible;
    public bool isRemoved;

    public void TakeDamage (float amount)
    {
        health -= amount;
        if(health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        if (destructible)
        {
            // Spawn a shattered object
            Instantiate(destroyedVersion, transform.position, transform.rotation);
            // Remove the current object
            Destroy(gameObject);
        } else if (isRemoved)
        {
            // Spawn a shattered object
            Destroy(Instantiate(destroyedVersion, transform.position, transform.rotation), 2);
            // Remove the current object
            Destroy(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
