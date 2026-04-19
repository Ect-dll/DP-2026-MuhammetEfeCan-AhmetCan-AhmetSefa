using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int attackDamage = 25;
    public float attackRange = 1.5f;
    public LayerMask enemyLayer;
    public Transform attackPoint;
    public GameObject swordVisual; // Kýlýç görselini buraya sürükle

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E TUSUNA BASILDI!"); // Konsolda bu yazýyý görmen lazým
            Attack();
        }
    }

    void Attack()
    {
        // Kýlýcý görünür yap
        if (swordVisual != null) swordVisual.SetActive(true);

        // 0.1 saniye sonra kýlýcý tekrar gizle (Sallama efekti gibi)
        Invoke("HideSword", 0.1f);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Health enemyHealth = enemy.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attackDamage);
            }
        }
    }

    void HideSword()
    {
        if (swordVisual != null) swordVisual.SetActive(false);
    }
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.white; // Rengi belirginleþtirelim
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}