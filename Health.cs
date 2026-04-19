using UnityEngine;
using UnityEngine.UI; // UI kütüphanesini eklemeyi unutma

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public Slider healthBar; // Unity panelinden Slider'ý buraya sürükle

    void Start()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = maxHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Alt kýsýmdaki konsolda bu yazýyý göreceksin:
        Debug.Log("Düþmana " + damage + " hasar verildi! Kalan Can: " + currentHealth);

        if (healthBar != null) healthBar.value = currentHealth;

        if (currentHealth <= 0) Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }
}