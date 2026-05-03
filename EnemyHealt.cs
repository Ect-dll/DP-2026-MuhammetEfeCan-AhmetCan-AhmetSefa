using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 30;
    private int currentHealth;

    [Header("Can Barı")]
    public Image healthBarFill;

    [Header("Ölüm Yazısı")]
    public GameObject olumYazisi; // Inspector'dan bir Text/TextMeshPro objesi sürükle

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();

        // Ölüm yazısını başta gizle
        if (olumYazisi != null)
            olumYazisi.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " hasar aldı! Kalan Can: " + currentHealth);

        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            StartCoroutine(OlumSureci());
        }
    }

    void UpdateHealthBar()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = (float)currentHealth / maxHealth;
            Debug.Log("Can barı güncellendi: " + healthBarFill.fillAmount);
        }
        else
        {
            Debug.LogWarning(gameObject.name + ": healthBarFill atanmamış!");
        }
    }

    IEnumerator OlumSureci()
    {
        // Hareketi durdur
        var rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.linearVelocity = Vector2.zero;

        var ai = GetComponent<EnemyAI>();
        if (ai != null) ai.enabled = false;

        // Ölüm yazısını göster
        if (olumYazisi != null)
            olumYazisi.SetActive(true);

        // 1 saniye bekle
        yield return new WaitForSeconds(1f);

        Debug.Log(gameObject.name + " öldü!");
        Destroy(gameObject);
    }
}