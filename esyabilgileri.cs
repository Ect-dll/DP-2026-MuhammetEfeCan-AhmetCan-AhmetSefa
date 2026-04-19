using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class esyabilgileri : MonoBehaviour
{
    [Header("Eþya Ayarlarý")]
    public int id; // Eþyanýn benzersiz kimliði (Örn: 1=Elmas, 2=Kalp)
    public Sprite itemresim; // Envanterde görünecek olan resim

    private void Start()
    {
        // Eðer yanlýþlýkla Sprite eklemeyi unuttuysan konsolda seni uyarsýn
        if (itemresim == null)
        {
            Debug.LogWarning(gameObject.name + " objesinde eþya resmi eksik!");
        }
    }
}