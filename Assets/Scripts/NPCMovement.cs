using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public float speed = 1.5f;
    public bool isWalking = false;
    public bool isTalking;
    public float walkTime = 1.5f;
    public float waitTime = 4.0f;
    public BoxCollider2D zone;

    private float waitCounter;
    private float walkCounter;
    private Rigidbody2D _rigidbody;
    private Animator _animator;

    private Vector2[] walkingDirections = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
    private int forbiddenDirection = -1;
    private int currentDirection;

    private DialogManager dialogManager;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        waitCounter = waitTime;
        walkCounter = walkTime;
        isTalking = false;
        dialogManager = FindObjectOfType<DialogManager>();
    }

    void FixedUpdate()
    {
        if(isTalking)
        {
            isTalking = dialogManager.dialogueActive;
            StopWalking();
            return;
        }

        if (isWalking)
        {
            if (zone != null) { ZoneControl(); }
            walkCounter -= Time.fixedDeltaTime;
            if (walkCounter < 0)
            {
                StopWalking();
            }
        }
        else
        {
            waitCounter -= Time.fixedDeltaTime;
            if (waitCounter < 0)
            {
                StartWalking();
            }
        }
    }

    void LateUpdate()
    {
        _animator.SetBool("Walking", isWalking);
        _animator.SetFloat("Horizontal", walkingDirections[currentDirection].x);
        _animator.SetFloat("Vertical", walkingDirections[currentDirection].y);
    }

    private void ZoneControl()
    {
        
        if(
            transform.position.x <= zone.bounds.min.x ||
            transform.position.x >= zone.bounds.max.x ||
            transform.position.y <= zone.bounds.min.y ||
            transform.position.y >= zone.bounds.max.y)
        {
            forbiddenDirection = currentDirection;
            StopWalking();
        }
    }

    public void StartWalking()
    {
        currentDirection = Random.Range(0, walkingDirections.Length);
        if (currentDirection != forbiddenDirection)
        {
            _rigidbody.velocity = walkingDirections[currentDirection] * speed;
            isWalking = true;
            walkCounter = walkTime;
            forbiddenDirection = -1;
        }
    }

    public void StopWalking()
    {
        isWalking = false;
        waitCounter = waitTime;
        _rigidbody.velocity = Vector2.zero;
    }
}
