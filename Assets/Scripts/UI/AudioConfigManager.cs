using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioConfigManager : MonoBehaviour
{
    // Slider que regula el volumen de los SFX
    private Slider sldSFX;
    // Slider que regula el volumen de la Musica
    private Slider sldMusic;

    private void Start()
    {
        sldSFX = transform.Find("sldSFX").GetComponent<Slider>();
        sldMusic = transform.Find("sldMusic").GetComponent<Slider>();
    }

    public void ResetConfig()
    {
        sldSFX.value = 0.8f;
        sldMusic.value = 0.8f;
    }

    public void SaveConfig()
    {
        gameObject.SetActive(false);
    }

    public void OpenConfig()
    {
        gameObject.SetActive(true);
    }
}
