using UnityEngine;

public class EnemyEventManager : MonoBehaviour
{
    public interface IAttackHitEvent
    {
        void AttackHitEvent();
    }

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
