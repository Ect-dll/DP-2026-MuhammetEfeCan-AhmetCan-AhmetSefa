using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PLAY : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("Play butonuna tıklandı!");
        SceneManager.LoadScene("SampleScene"); // Build Settings'teki sahne adını yazın
    }

    public void ExitGame()
    {
        // Oyundan çık
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Editor'de test için
        #else
            Application.Quit(); // Oyun build'inde çık
        #endif
    }
}
