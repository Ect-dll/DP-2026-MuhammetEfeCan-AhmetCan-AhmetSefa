using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float moveSpeed;
    public float followRange;
    public float attackRange;
    public int damage = 10;
    public float attackCooldown = 1f;

    [Header("Düşman Can Ayarları")]
    public int maxHealth = 50;
    private int currentHealth;
    private float lastAttackTime;
    private Rigidbody2D rb;

    private Vector3 baslangicBoyutu;

    void Start()
    {
        // --- TEST İÇİN DEĞERLERİ ZORLA VERİYORUZ (Inspector'ı ezer) ---
        moveSpeed = 40f;      // Hızı zorla 40 yaptık
        followRange = 500f;   // Dünyanın öbür ucundan görsün
        attackRange = 4f;     // Vurma mesafesini garantiye aldık
        // --------------------------------------------------------------

        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        baslangicBoyutu = transform.localScale;

        if (rb.gravityScale == 0f)
        {
            rb.gravityScale = 3f;
        }

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        // KARAKTERİ BULMA TESTİ
        if (player == null)
        {
            GameObject target = GameObject.Find("karakter");
            if (target != null)
            {
                player = target.transform;
                Debug.Log(gameObject.name + ": Karakteri buldum! Hedefe kilitlendim.");
            }
            else
            {
                Debug.LogError(gameObject.name + ": DİKKAT! 'karakter' isimli objeyi sahnede bulamıyorum! İsmi yanlış olabilir.");
            }
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

        rb.linearVelocity = new Vector2(direction.x * moveSpeed, rb.linearVelocity.y);

        if (direction.x > 0)
            transform.localScale = new Vector3(Mathf.Abs(baslangicBoyutu.x), baslangicBoyutu.y, baslangicBoyutu.z);
        else if (direction.x < 0)
            transform.localScale = new Vector3(-Mathf.Abs(baslangicBoyutu.x), baslangicBoyutu.y, baslangicBoyutu.z);
    }

    void StopMoving()
    {
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }

    void AttackPlayer()
    {
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);

        if (Time.time >= lastAttackTime + attackCooldown)
        {
            PlayerHealth health = player.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(damage);
                Debug.Log("Düşman GÜM diye vurdu!");
            }
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