using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 2f;
    public float followRange = 8f;
    public float attackRange = 1.2f;
    public int damage = 10;
    public float attackCooldown = 1f;

    [Header("Düşman Can Ayarları")]
    public int maxHealth = 50;
    private int currentHealth;
    private float lastAttackTime;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;

        // Yerçekimi kontrolü — bu olmazsa düşman havada kalır
        if (rb.gravityScale == 0f)
        {
            rb.gravityScale = 3f; // İstediğin değere ayarla
            Debug.LogWarning(gameObject.name + ": GravityScale 0'dı, 3 yapıldı!");
        }

        // Rigidbody2D Constraints: Rotation Z'yi kilitle (devrilmesin)
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        if (player == null)
        {
            GameObject target = GameObject.Find("karakter");
            if (target != null)
                player = target.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= followRange && distance > attackRange)
            FollowPlayer();
        else
            StopMoving();

        if (distance <= attackRange)
            AttackPlayer();
    }

    void FollowPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;

        // Y'ye dokunma → yerçekimi çalışsın
        rb.linearVelocity = new Vector2(direction.x * moveSpeed, rb.linearVelocity.y);

        if (direction.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (direction.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    void StopMoving()
    {
        // Sadece X'i durdur, Y'yi (yerçekimi) serbest bırak
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }

    void AttackPlayer()
    {
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);

        if (Time.time >= lastAttackTime + attackCooldown)
        {
            PlayerHealth health = player.GetComponent<PlayerHealth>();
            if (health != null)
                health.TakeDamage(damage);

            lastAttackTime = Time.time;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log(gameObject.name + " hasar aldı! Kalan can: " + currentHealth);

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        Debug.Log("Düşman öldü!");
        Destroy(gameObject);
    }
}