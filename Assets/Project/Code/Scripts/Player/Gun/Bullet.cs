using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    private Rigidbody2D rb;
    public float damage = 3f;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = transform.up * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EventManager.EmitEnemyHit(damage);
            gameObject.SetActive(false);
        }
    }
}
