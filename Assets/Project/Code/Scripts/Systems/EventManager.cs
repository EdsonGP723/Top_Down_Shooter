using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void OnPlayerHitEvent(float damage);
    public static event OnPlayerHitEvent OnPlayerHit;
    public delegate void OnEnemyHitEvent(float damage);
    public static event OnEnemyHitEvent OnEnemyHit;

    public static void EmitPlayerHit(float damage)
    {
        if (OnPlayerHit != null)
        {
            OnPlayerHit(damage);
        }
    }
    public static void EmitEnemyHit(float damage)
    {
        if (OnEnemyHit != null)
        {
            OnEnemyHit(damage);
        }
    }
}
