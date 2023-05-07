using UnityEngine;

public class EnemyAttack : MonoBehaviour, EnemyEventManager.IAttackHitEvent
{
    [SerializeField] float damage = 40f;
    PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    public void AttackHitEvent()
    {
        if (playerHealth == null) return;
        playerHealth.TakeDamage(damage);
        playerHealth.GetComponent<DisplayDamage>().ShowDamageImpact();
    }

    public void OnDamageTaken()
    {
        Debug.Log("Enemy has been atacked");
    }
}
