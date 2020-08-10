using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public const int MAX_STAT = 100;
    public const int MAX_HEALTH = 9999;

    public int level;
    public int exp;
    public int[] expToLevelUp;

    //TODO: Pasar a carga por JSON y prt
    [Tooltip("Puntos de salud por nivel")]
    public int[] hpLevels;
    [Tooltip("Puntos de fuerza por nivel")]
    public int[] strengthLevels;
    [Tooltip("Puntos de defensa por nivel")]
    public int[] defenseLevels;
    [Tooltip("Puntos de velocidad de ataque por nivel")]
    public int[] speedLevels;
    [Tooltip("Puntos de suerte (en realacion al ataque y la defensa)")]
    public int[] luckLevels;
    [Tooltip("Puntos de precision (probabilidad de critico)")]
    public int[] accuracyLevels;

    private HealthManager healthManager;
    private PlayerController playerController;

    private void Start()
    {
        healthManager = GetComponent<HealthManager>();
        playerController = GetComponent<PlayerController>();

        healthManager.UpdateMaxHealth(hpLevels[level]);

        if(gameObject.tag.Equals("Enemy"))
        {
            EnemyController enemyController = GetComponent<EnemyController>();
            enemyController.speed += speedLevels[level] / MAX_STAT;
        }
    }

    public void addExperience(int exp)
    {
        this.exp += exp;

        if (level >= expToLevelUp.Length) return;
        
        if (exp >= expToLevelUp[level])
        {
            level++;
            healthManager.UpdateMaxHealth(hpLevels[level]);

            Debug.Log(speedLevels[level] / MAX_STAT);
            playerController.attackTime -= speedLevels[level] / MAX_STAT;
        }
    }
}
