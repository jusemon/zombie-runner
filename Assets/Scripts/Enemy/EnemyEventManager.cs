using UnityEngine;

public interface IAttackHitEvent
{
    void AttackHitEvent();
}

public class EnemyEventManager : MonoBehaviour
{
    private IAttackHitEvent[] components;

    private void Start()
    {
        components = GetComponentsInParent<IAttackHitEvent>();
    }

    public void AttackHitEvent()
    {
        foreach (var component in components)
        {
            component.AttackHitEvent();
        }
    }
}
