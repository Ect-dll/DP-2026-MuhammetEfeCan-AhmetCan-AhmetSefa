using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Sağlık Ayarları")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("Can Barı")]
    public Slider healthBarSlider;  // Slider kullanıyorsan bunu bağla
    public Image healthBarFill;     // Image/Fill kullanıyorsan bunu bağla

    [Header("Düşman Kontrol Ayarları")]
    public float logInterval = 5f;
    public float checkRadius = 20f;
    public LayerMask enemyLayer;

    private float timer = 0f;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= logInterval)
        {
            timer = 0f;
            CheckEnemiesAndLog();
        }
    }

    void CheckEnemiesAndLog()
    {
        // 2D için OverlapCircleAll kullan
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, checkRadius, enemyLayer);

        if (hitColliders.Length > 0)
        {
            Debug.Log("⚠️ DİKKAT: Bölgede düşman var! Oyuncu Canı: " + currentHealth);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // 0'ın altına düşmesin
        Debug.Log("Karakter hasar aldı: " + damage + " | Kalan can: " + currentHealth);

        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Can barını iki yöntemle de güncelleyebilir
    void UpdateHealthBar()
    {
        float oran = (float)currentHealth / maxHealth;

        if (healthBarSlider != null)
        {
            healthBarSlider.maxValue = maxHealth;
            healthBarSlider.value = currentHealth;
        }

        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = oran;
        }
    }

    void Die()
    {
        Debug.Log("Karakter öldü! Sahne yeniden yükleniyor...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}