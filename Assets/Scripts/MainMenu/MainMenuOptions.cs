using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuOptions : MonoBehaviour
{
    public AudioConfigManager audioConfigManager;
    public GameObject creditsPanel;

    public void StartGame()
    {
        SceneManager.LoadScene("Introduction");
    }

    public void ShowConfig()
    {
        audioConfigManager.OpenConfig();
    }

    public void ShowCredits()
    {
        creditsPanel.SetActive(true);
    }

    public void HideCredits()
    {
        creditsPanel.SetActive(false);
    }
}
