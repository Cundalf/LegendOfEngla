using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
[RequireComponent(typeof(HealthManager))]
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
{
    [Tooltip("Velocidad de movimiento del enemigo")]
    public float speed = 1;

    [Tooltip("Tiempo entre pasos sucesivos")]
    public float timeBetweenSteps;
    private float timeBetweenStepsCounter;

    [Tooltip("Tiempo entre pasos")]
    public float timeToMakeStep;
    private float timeToMakeStepCounter;

    [Tooltip("(Opcional) Zona que limita el movimiento del enemigo")]
    public BoxCollider2D zone;

    private Rigidbody2D _rigibody;
    private bool isMoving;
    private Vector2 directionToMove;
    private Vector2 forbiddenDirection;

    void Start()
    {
        _rigibody = GetComponent<Rigidbody2D>();
        timeBetweenStepsCounter = timeBetweenSteps * Random.Range(0.5f, 1.51f);
        timeToMakeStepCounter = timeToMakeStep * Random.Range(0.5f, 1.51f);

        forbiddenDirection = new Vector2(9, 9);
    }

    void Update()
    {
        if(isMoving)
        {
            if (zone != null) { ZoneControl(); }

            timeToMakeStepCounter -= Time.deltaTime;
            if(timeToMakeStepCounter < 0)
            {
                StopMoving();
            }
        }
        else
        {
            if( timeBetweenStepsCounter < 0 )
            {
                StartMoving();
            }
            else
            {
                timeBetweenStepsCounter -= Time.deltaTime;
            }
        }
    }

    private void ZoneControl()
    {
        if (transform.position.x <= zone.bounds.min.x ||
            transform.position.x >= zone.bounds.max.x)
        {
            Debug.Log("Me fui en X");
            forbiddenDirection.x = directionToMove.x;
            StopMoving();
        }

        if (transform.position.y <= zone.bounds.min.y ||
            transform.position.y >= zone.bounds.max.y)
        {
            Debug.Log("Me fui en Y");
            forbiddenDirection.y = directionToMove.y;
            StopMoving();
        }
    }

    private void StartMoving()
    {
        directionToMove = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2));

        Debug.Log(directionToMove);
        if ((directionToMove.x != forbiddenDirection.x && directionToMove.y != forbiddenDirection.y))
        {
            _rigibody.velocity = directionToMove * speed;
            isMoving = true;
            timeToMakeStepCounter = timeToMakeStep;
            forbiddenDirection = new Vector2(9, 9);
        }
    }

    private void StopMoving()
    {
        isMoving = false;
        timeBetweenStepsCounter = timeBetweenSteps;
        _rigibody.velocity = Vector2.zero;
    }
}
