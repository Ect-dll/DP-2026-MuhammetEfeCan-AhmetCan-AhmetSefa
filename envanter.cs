using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class envanter : MonoBehaviour
{
    public List<itemler> itemleri = new List<itemler>();
    public GameObject envanteri;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("item"))
        {
            esyabilgileri esya = collision.GetComponent<esyabilgileri>();
            if (esya == null) return;

            // DÝKKAT: Esya ID'si slot numarasýný belirler (ID 0 ise 1. slot, ID 1 ise 2. slot...)
            int slotIndeksi = esya.id;

            // Eðer ID, slot sayýmýzdan büyükse hata vermemesi için kontrol
            if (slotIndeksi < itemleri.Count)
            {
                // Eþya bilgilerini listeye kaydet
                itemleri[slotIndeksi].itemid = esya.id;
                itemleri[slotIndeksi].itemresim = esya.itemresim;
                itemleri[slotIndeksi].itemsayý++;

                // UI Güncelleme (Direkt ilgili indeksteki çocuðu bulur)
                if (envanteri.transform.childCount > slotIndeksi)
                {
                    Image slotResmi = envanteri.transform.GetChild(slotIndeksi).GetComponent<Image>();
                    slotResmi.sprite = esya.itemresim;
                    slotResmi.color = Color.white; // Görünür yap
                }

                Destroy(collision.gameObject);
            }
        }
    }
    [System.Serializable]
    public class itemler
    {
        public Sprite itemresim;
        public int itemsayý;
        public int itemid;

        // Eðer kodda constructor kullanýyorsak bu da lazým:
        public itemler(Sprite resim, int id, int sayi)
        {
            itemresim = resim;
            itemid = id;
            itemsayý = sayi;
        }
    }
}