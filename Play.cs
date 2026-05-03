using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLAY : MonoBehaviour
{
    [Header("Oyun Baþlama Ayarlarý")]
    public GameObject anaMenuArayuzu;
    public Transform karakter;
    public Transform baslangicNoktasi;

    [Header("Kamera Ayarlarý")]
    public Transform anaKamera; // Kamerayý buraya ekledik

    public void PlayGame()
    {
        Debug.Log("Play butonuna týklandý! Ayný sahnede oyun baþlýyor.");

        // 1. Menüyü ekrandan gizle
        if (anaMenuArayuzu != null)
        {
            anaMenuArayuzu.SetActive(false);
        }

        // 2. Karakteri hedef noktaya ýþýnla
        if (karakter != null && baslangicNoktasi != null)
        {
            karakter.position = baslangicNoktasi.position;
        }

        // 3. Kamerayý da karakterin gittiði yere ýþýnla
        if (anaKamera != null && baslangicNoktasi != null)
        {
            // 2D oyunlarda kameranýn Z ekseni her zaman -10'da kalmalýdýr, yoksa ekran siyah olur!
            anaKamera.position = new Vector3(baslangicNoktasi.position.x, baslangicNoktasi.position.y, -10f);
        }
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; 
#else
        Application.Quit();
#endif
    }
}