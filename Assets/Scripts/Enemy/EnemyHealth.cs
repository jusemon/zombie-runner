using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;
    private bool isDead = false;

    public void TakeDamage(float damage)
    {
        if (isDead) return;
        BroadcastMessage("OnDamageTaken");

        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            Die();
        }
    }

    public bool IsDead()
    {
        return isDead;
    }

    private void Die()
    {
        isDead = true;
        GetComponentInChildren<Animator>().SetTrigger("die");
    }
}
