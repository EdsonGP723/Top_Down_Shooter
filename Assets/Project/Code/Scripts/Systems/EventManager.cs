using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void OnPlayerHitEvent(float damage);
    public static event OnPlayerHitEvent OnPlayerHit;

    public static void EmitPlayerHit(float damage)
    {
        if (OnPlayerHit != null)
        {
            OnPlayerHit(damage);
        }
    }
}
