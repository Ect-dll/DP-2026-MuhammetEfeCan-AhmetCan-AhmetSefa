using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Sağlık Ayarları")]
    public int maxHealth = 100;
    public int currentHealth;
    public Slider healthBar;

    [Header("Düşman Kontrol Ayarları")]
    public float logInterval = 5f;  // Kaç saniyede bir yazacağı
    public float checkRadius = 20f; // Düşmanları arayacağı mesafe yarıçapı
    private float timer = 0f;

    void Start()
    {
        currentHealth = maxHealth;

        // Can barı UI ayarlamaları
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = maxHealth;
        }
    }

    void Update()
    {
        // 5 saniyelik zamanlayıcıyı çalıştır
        timer += Time.deltaTime;

        if (timer >= logInterval)
        {
            timer = 0f; // Zamanlayıcıyı sıfırla
            CheckEnemiesAndLog();
        }
    }

    // Etrafta düşman olup olmadığını kontrol eden fonksiyon
    void CheckEnemiesAndLog()
    {
        // Karakterin etrafında 'checkRadius' kadar bir alan tarar
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRadius);
        bool dusmanVarMi = false;

        foreach (Collider hit in hitColliders)
        {
            // Eğer taranan alan içinde "Enemy" tag'ine sahip biri varsa
            if (hit.CompareTag("Enemy"))
            {
                dusmanVarMi = true;
                break; // Bir düşman bulmak yeterli, aramayı durdur
            }
        }

        // Eğer yakında en az bir düşman varsa canı konsola yazdır
        if (dusmanVarMi)
        {
            Debug.Log("⚠️ DİKKAT: Bölgede düşman var! Oyuncu Canı: " + currentHealth);
        }
    }

    // Karakter hasar aldığında çalışan fonksiyon
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Karakter hasar aldi: " + damage + " | Kalan can: " + currentHealth);

        // Can barını güncelle
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        // Can sıfırlandıysa öl
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Karakter öldü! Sahne yeniden yükleniyor...");

        // Sahneyi yeniden yükle
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Unity Editöründe düşman arama mesafeni kırmızı bir küre olarak görmeni sağlar
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}