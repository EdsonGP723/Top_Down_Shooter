using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 direccion;
    private float velocidad;

    public float damage = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ConfigurarMovimiento(Vector2 nuevaDireccion, float nuevaVelocidad)
    {
        direccion = nuevaDireccion.normalized;
        velocidad = nuevaVelocidad;

        // Asignar la velocidad al Rigidbody2D
        rb.linearVelocity = direccion * velocidad;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            EventManager.EmitPlayerHit(damage);
            gameObject.SetActive(false);
        }
    }
}
