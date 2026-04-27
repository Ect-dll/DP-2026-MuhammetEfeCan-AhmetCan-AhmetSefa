using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int attackDamage = 25;
    public float attackRange = 1.5f;
    public LayerMask enemyLayer;
    public Transform attackPoint;
    public GameObject swordVisual;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Attack();
        }
    }

    void Attack()
    {
        // 1. GÜVENLÝK KONTROLÜ: Eðer Unity arayüzünden attackPoint sürüklenmediyse,
        // kýrmýzý hata vermek yerine kodu burada zararsýzca durdurur.
        if (attackPoint == null)
        {
            Debug.LogWarning("DÝKKAT: attackPoint boþ! Lütfen Unity Inspector'dan bir obje sürükleyin.");
            return;
        }

        // 2. MANTIK DÜZELTMESÝ: Saldýrýrken kýlýç GÖRÜNÜR olmalý (true).
        if (swordVisual != null) swordVisual.SetActive(true);

        // 0.1 saniye sonra HideSword metodunu çaðýrýp kýlýcý gizler.
        Invoke("HideSword", 0.1f);

        // Belirlenen alandaki düþmanlarý algýla
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            // 3. ÝSÝMLENDÝRME: Sýnýf adý ile deðiþken adý karýþmasýn diye küçük harfle baþlattýk.
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attackDamage);
            }
        }
    }

    void HideSword()
    {
        // Kýlýcý tekrar gizle
        if (swordVisual != null) swordVisual.SetActive(false);
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}