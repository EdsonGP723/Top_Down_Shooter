using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int value = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StoreSystem.instance.AddCoins(value);
            Destroy(gameObject);
        }
    }
}
