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

    //TODO: Agregar tooltips
    public int[] hpLevels;
    public int[] strengthLevels;
    public int[] defenseLevels;
    public int[] speedLevels;
    public int[] luckLevels;
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
            playerController.attackTime -= speedLevels[level] / MAX_STAT;
        }
    }
}
