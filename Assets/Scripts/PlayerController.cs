using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    // Public Variable
    [Tooltip("ID del destino")]
    public string nextUUID;

    [Tooltip("Velocidad estandar del jugador")]
    public const float speed = 3.5f;

    [Tooltip("Velocidad del jugador al correr")]
    public const float Runningspeed = 5.0f;

    [Tooltip("Configuracion del facing del personaje")]
    public Vector2 lastMovement = Vector2.zero;

    [Tooltip("Activa o desactiva el movimiento del personaje")]
    public bool canMove = true;

    // Para el control del DontDestroyOnLoad.
    // TODO: Pasar a otra alternativa
    public static bool playerCreated;

    // Private Variables
    private bool walking = false;
    private bool attacking = false;
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    private float attackTimeCounter;
    private const string AXIS_H = "Horizontal",
                            AXIS_V = "Vertical",
                            WALK = "Walking",
                            ATTACKING = "Attacking",
                            LASTH = "LastH",
                            LASTV = "LastV";

    public bool IsAttacking
    {
        get
        {
            return attacking;
        }
    }

    public float attackTime;
    /*public float attackTime
    {
        set
        {
            if (value < 0)
            {
                attackTime = 0;
            }
            else
            {
                attackTime = value;
            }
        }
        get
        {
            return attackTime;
        }
    }*/


    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        playerCreated = true;
    }

    void Update()
    {
        walking = false;
        if (!canMove)
        {
            _rigidbody2D.velocity = Vector2.zero;
            return;
        }
        
        if (attacking)
        {
            attackTimeCounter -= Time.deltaTime;
            if (attackTimeCounter < 0)
            {
                attacking = false;
                _animator.SetBool(ATTACKING, false);
            }
        }
        else
        {
            // Movimiento Horizontal
            if (Mathf.Abs(Input.GetAxisRaw(AXIS_H)) > 0.2f)
            {
                _rigidbody2D.velocity = new Vector2(Input.GetAxisRaw(AXIS_H), _rigidbody2D.velocity.y).normalized * speed;
                lastMovement = new Vector2(Input.GetAxisRaw(AXIS_H), 0);
                walking = true;
            }
            else
            {
                _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
            }


            // Movimiento vertical
            if (Mathf.Abs(Input.GetAxisRaw(AXIS_V)) > 0.2f)
            {
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, Input.GetAxisRaw(AXIS_V)).normalized * speed;
                lastMovement = new Vector2(0, Input.GetAxisRaw(AXIS_V));
                walking = true;
            }
            else
            {
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0);
            }

            // Control de ataque
            if (Input.GetButton("Attack") && !walking)
            {
                SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.ATTACK);
                attacking = true;
                attackTimeCounter = attackTime;
                _rigidbody2D.velocity = Vector2.zero;
                _animator.SetBool(ATTACKING, true);
            }
        }
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
