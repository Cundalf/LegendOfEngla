using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class UIManager : MonoBehaviour
{
    public Slider playerHealthBar;
    public Slider playerExpBar;
    public TMP_Text playerHealthText;
    public TMP_Text playerLevelText;
    public HealthManager playerHealthManager;
    public CharacterStats playerStats;

    // TODO: Pasar a subscripcion de eventos
    void Update()
    {
        playerHealthBar.maxValue = playerHealthManager.maxHealth;
        playerHealthBar.value = playerHealthManager.Health;

        StringBuilder stringBuilder = new StringBuilder()
            .Append("HP: ")
            .Append(playerHealthManager.Health)
            .Append(" / ")
            .Append(playerHealthManager.maxHealth);

        playerHealthText.text = stringBuilder.ToString();

        stringBuilder = new StringBuilder().Append("Nivel: ").Append(playerStats.level);
        playerLevelText.text = stringBuilder.ToString();

        if(playerStats.level >= playerStats.expToLevelUp.Length)
        {
            playerExpBar.enabled = false;
            return;
        }

        playerExpBar.maxValue = playerStats.expToLevelUp[playerStats.level];
        playerExpBar.minValue = playerStats.expToLevelUp[playerStats.level - 1];
        playerExpBar.value = playerStats.exp;
    }
}
