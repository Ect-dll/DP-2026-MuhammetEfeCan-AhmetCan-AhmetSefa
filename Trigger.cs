using UnityEngine;

public class Trigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        { // Düþmanýn Tag'ini "Enemy" yapmayý unutma!
            other.GetComponent<EnemyHealth>().TakeDamage(10);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
