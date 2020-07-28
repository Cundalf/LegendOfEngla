using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [Tooltip("Daño provocado")]
    public int damage;

    public GameObject bloodAnim;
    public GameObject damageCanvas;
    public string WeaponName;
    private GameObject hitPoit;
    private CharacterStats stats;
    private PlayerController playerController;

    private void Start()
    {
        hitPoit = transform.Find("HitPoint").gameObject;

        GameObject playerGO = GameObject.Find("Player");
        stats = playerGO.GetComponent<CharacterStats>();
        playerController = playerGO.GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (!collision.gameObject.tag.Equals("Enemy") || !playerController.attacking) return;
        
        CharacterStats enemyStats = collision.gameObject.GetComponent<CharacterStats>();

        float playerFactor = (1 + stats.strengthLevels[stats.level] / CharacterStats.MAX_STAT);
        float enemyFactor = (1 - enemyStats.defenseLevels[enemyStats.level] / CharacterStats.MAX_STAT);

        int totalDamage = (int)(damage * enemyFactor * playerFactor);

        if(Random.Range(0, CharacterStats.MAX_STAT) < stats.accuracyLevels[stats.level])
        {
            if(Random.Range(0, CharacterStats.MAX_STAT) < enemyStats.luckLevels[stats.level])
            {
                totalDamage = 0;
            }
            else
            {
                totalDamage *= 5;
            }
        }

        if (bloodAnim != null && hitPoit != null)
        {
            Destroy( 
                    Instantiate(
                        bloodAnim, 
                        hitPoit.transform.position, 
                        hitPoit.transform.rotation
                    ),
                    0.5f
            );
        }

        var clone = (GameObject) Instantiate(
            damageCanvas, 
            hitPoit.transform.position, 
            Quaternion.Euler(Vector3.zero)
        );
        clone.GetComponent<DamageNumber>().damagePoints = totalDamage;

        collision.gameObject.GetComponent<HealthManager>().DamageCharacter(totalDamage);

    }

}
