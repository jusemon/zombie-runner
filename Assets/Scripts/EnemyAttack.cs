using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
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
    }

    public void OnDamageTaken()
    {
        Debug.Log("Enemy has been atacked");
    }
}
