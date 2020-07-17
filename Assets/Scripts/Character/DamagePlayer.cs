using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    [Tooltip("Tiempo que tarda en revivir el personaje")]
    public float timeToRevivePlayer;

    [Tooltip("Daño provocado")]
    public int damage;

    public GameObject damageCanvas;
    private CharacterStats playerStats;
    private CharacterStats _stats;

    private void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<CharacterStats>();
        _stats = GetComponent<CharacterStats>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if( collision.gameObject.name.Equals("Player") )
        {
            float enemyFactor = 1 + _stats.strengthLevels[_stats.level] / CharacterStats.MAX_STAT;
            float playerFactor = 1 - playerStats.defenseLevels[playerStats.level] / CharacterStats.MAX_STAT;

            int totalDamage = Mathf.Clamp((int)(damage * enemyFactor * playerFactor), 1, CharacterStats.MAX_HEALTH);

            if(Random.Range(0, 100) < playerStats.luckLevels[playerStats.level])
            {
                if (Random.Range(0, CharacterStats.MAX_STAT) > _stats.accuracyLevels[_stats.level])
                {
                    totalDamage = 0;
                }
            }

            var clone = (GameObject)Instantiate(
                damageCanvas,
                collision.transform.position,
                Quaternion.Euler(Vector3.zero)
            );
            clone.GetComponent<DamageNumber>().damagePoints = totalDamage;

            collision.gameObject.GetComponent<HealthManager>().DamageCharacter(totalDamage);
        }
    }
}
