using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private Rigidbody2D _rigibody;
    private bool isMoving;
    private Vector2 directionToMove;

    // Start is called before the first frame update
    void Start()
    {
        _rigibody = GetComponent<Rigidbody2D>();
        timeBetweenStepsCounter = timeBetweenSteps * Random.Range(0.5f, 1.51f);
        timeToMakeStepCounter = timeToMakeStep * Random.Range(0.5f, 1.51f);
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving)
        {
            timeToMakeStepCounter -= Time.deltaTime;
            _rigibody.velocity = directionToMove * speed;

            if(timeToMakeStepCounter < 0)
            {
                isMoving = false;
                timeBetweenStepsCounter = timeBetweenSteps;
                _rigibody.velocity = Vector2.zero;
            }
        }
        else
        {
            timeBetweenStepsCounter -= Time.deltaTime;

            if( timeBetweenStepsCounter <0 )
            {
                isMoving = true;
                timeToMakeStepCounter = timeToMakeStep;
                directionToMove = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2));
            }
        }
    }
}
