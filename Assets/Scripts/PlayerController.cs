using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public string nextUUID;
    public float speed = 5.0f;
    //public float currentSpeed = 5.0f;
    public Vector2 lastMovement = Vector2.zero;
    public static bool playerCreated;
    public bool canMove = true;
    public bool isTalking;

    private bool walking = false;
    private bool attacking = false;
    private const string    AXIS_H = "Horizontal", 
                            AXIS_V = "Vertical",
                            WALK = "Walking",
                            ATTACKING = "Attacking",
                            LASTH = "LastH", 
                            LASTV = "LastV";
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;

    public float attackTime;
    private float attackTimeCounter;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        playerCreated = true;
        isTalking = false;
    }

    void Update()
    {
        if(isTalking)
        {
            _rigidbody2D.velocity = Vector2.zero;
            return;
        }

        walking = false;

        if (!canMove) return;

        if(attacking)
        {
            attackTimeCounter -= Time.deltaTime;
            if(attackTimeCounter < 0)
            {
                attacking = false;
                _animator.SetBool(ATTACKING, false);
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                attacking = true;
                attackTimeCounter = attackTime;
                _rigidbody2D.velocity = Vector2.zero;
                _animator.SetBool(ATTACKING, true);
            }
        }

        if(Mathf.Abs(Input.GetAxisRaw(AXIS_H)) > 0.2f)
        {
            //float velocity = Input.GetAxisRaw(AXIS_H) * speed;
            //_rigidbody2D.velocity = new Vector2(velocity, _rigidbody2D.velocity.y);
            _rigidbody2D.velocity = new Vector2(Input.GetAxisRaw(AXIS_H), _rigidbody2D.velocity.y).normalized * speed;
            lastMovement = new Vector2(Input.GetAxisRaw(AXIS_H), 0);
            walking = true;
        }
        else
        {
            _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
        }

        if (Mathf.Abs(Input.GetAxisRaw(AXIS_V)) > 0.2f)
        {
            //float velocity = Input.GetAxisRaw(AXIS_V) * speed;
            //_rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, velocity);
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, Input.GetAxisRaw(AXIS_V)).normalized * speed;
            lastMovement = new Vector2(0, Input.GetAxisRaw(AXIS_V));
            walking = true;
        }
        else
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0);
        }

         /*
        if (Mathf.Abs(Input.GetAxisRaw(AXIS_H)) > 0.2f && Mathf.Abs(Input.GetAxisRaw(AXIS_V)) > 0.2f)
        {
            currentSpeed = speed / Mathf.Sqrt(2);
        }
        else
        {
            currentSpeed = speed;
        }*/
    }

    private void LateUpdate()
    {
        if(!walking)
        {
            _rigidbody2D.velocity = Vector2.zero;
        }

        _animator.SetFloat(AXIS_H, Input.GetAxisRaw(AXIS_H));
        _animator.SetFloat(AXIS_V, Input.GetAxisRaw(AXIS_V));
        _animator.SetBool(WALK, walking);
        _animator.SetFloat(LASTH, lastMovement.x);
        _animator.SetFloat(LASTV, lastMovement.y);
    }
}
