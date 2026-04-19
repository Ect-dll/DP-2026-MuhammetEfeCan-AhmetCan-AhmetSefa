using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    float horizontal;

    [Header("Ayarlar")]
    public float speed = 11f;       // Saða sola gitme hýzý
    public float jumpForce = 17f;   // Zýplama gücü

    bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Karakterin devrilmemesi için Z eksenini kodla sabitleme
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        // Tuþ giriþleri
        horizontal = Input.GetAxisRaw("Horizontal");

        // Zýplama Kontrolü (Space, W veya Yukarý Ok)
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        // HAREKET BURADA YAPILIYOR
        // linearVelocity kullanýrken Time.deltaTime ile ÇARPMA. 
        // Eðer çarparsan karakter yerinden oynamaz kadar yavaþ gider.
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
        }
    }
}