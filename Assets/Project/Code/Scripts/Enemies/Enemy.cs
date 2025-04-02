using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float life = 25;
    public float speed = 3f;
    public Transform target;
    public Transform shootPoint;
    public float stopDistance = 5f;
    private float bulletSpeed = 3f;
    private float lastShootTime = 0f;
    public float shootCooldown = 2f;

    private void Awake()
    {
        PlayerMovement player = FindAnyObjectByType<PlayerMovement>();
        if (player != null)
        {
            target = player.transform;
        }
    }

    private void FixedUpdate()
    {
        Vector2 direccion = (target.position - shootPoint.position).normalized;
        float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
        shootPoint.rotation = Quaternion.Euler(0, 0, angulo - 90f);
        if (Time.time >= lastShootTime + shootCooldown)
        {
            Shoot(direccion);
            lastShootTime = Time.time;
        }
    }
    private void Update()
    {
        Move();
    }

    public void Move()
    {
        // Movimiento básico hacia el objetivo
        if (target != null)
        {
            // Detenerse al alcanzar la distancia mínima
            float distanceToPlayer = Vector3.Distance(transform.position, target.position);

            if (distanceToPlayer > stopDistance)
            {
                Vector3 direction = (target.position - transform.position).normalized;
                transform.position += direction * speed * Time.deltaTime;
            }
        }
    }

    public void Shoot(Vector2 direccion)
    {
        GameObject bullet = EnemyBulletPool.instance.GetPooledObject();
        if (bullet != null)
        {
            // Configurar posición y rotación
            bullet.transform.position = shootPoint.position;
            bullet.transform.rotation = shootPoint.rotation;
            bullet.SetActive(true);
            // Configurar la dirección y velocidad usando el controlador del bullet
            EnemyBullet controlador = bullet.GetComponent<EnemyBullet>();
            if (controlador != null)
            {
                controlador.ConfigurarMovimiento(direccion, bulletSpeed);
            }
        }
    }

    void OnEnable()
    {
        EventManager.OnEnemyHit += TakeDamage;
    }
    void OnDisable()
    {
        EventManager.OnEnemyHit -= TakeDamage;
    }

    private void TakeDamage(float damage)
    {
        life -= damage;
        if (life <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        WaveManager enemy = FindAnyObjectByType<WaveManager>();
        enemy.enemiesAlive -= 1;
        gameObject.SetActive(false);
    }
}
