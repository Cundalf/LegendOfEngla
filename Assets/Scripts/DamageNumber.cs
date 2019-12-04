using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageNumber : MonoBehaviour
{
    public float damageSpeed;
    public float damagePoints;
    public TMP_Text damageTextPro;

    private Vector2 direction = new Vector2(1, 0);
    public float timeToChangeDirection = 1;
    private float timeToChangeDirectionCounter = 1;

    void Update()
    {
        timeToChangeDirectionCounter -= Time.deltaTime;
        if( timeToChangeDirectionCounter < timeToChangeDirection / 2 )
        {
            direction *= -1;
            timeToChangeDirectionCounter = timeToChangeDirection;
        }

        damageTextPro.text = "" + damagePoints;
        transform.position = new Vector3(
            transform.position.x + direction.x * damageSpeed * Time.deltaTime,
            transform.position.y + damageSpeed * Time.deltaTime,
            transform.position.z
        );

        transform.localScale = transform.localScale * ( 1 - Time.deltaTime / 3 );
    }
}
