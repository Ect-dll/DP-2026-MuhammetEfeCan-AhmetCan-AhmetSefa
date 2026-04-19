using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int damageAmount = 20;

    // 2D çarpýþma tetiklendiðinde çalýþýr
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Çarpýlan nesnede Health scripti var mý kontrol et
        Health targetHealth = collision.GetComponent<Health>();

        if (targetHealth != null)
        {
            targetHealth.TakeDamage(damageAmount);
        }
    }
}