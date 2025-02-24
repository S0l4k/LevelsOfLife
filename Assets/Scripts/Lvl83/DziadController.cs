using UnityEngine;

public class DziadController : MonoBehaviour
{
    private Rigidbody2D dziadRigidbody;
    private BoxCollider2D dziadCollider;
    private Vector2 movementDirection;
    public float initialMovementSpeed = 1f;
    public float minMovementSpeed = 0.3f;
    [SerializeField] private float speedDecayRate = 0.01f;
    private bool isWalking;
    private Animator animator;

    void Start()
    {
        
        animator = GetComponent<Animator>();
        dziadRigidbody = GetComponent<Rigidbody2D>();
        dziadCollider = GetComponent<BoxCollider2D>();

        
        isWalking = false;
        animator.SetBool("isWalking", isWalking);
    }

    void Update()
    {
        
        ProcessInputs();

        
        if (movementDirection != Vector2.zero)
        {
            isWalking = true; 
        }
        else
        {
            isWalking = false; 
        }

        
        animator.SetBool("isWalking", isWalking);

        
        if (initialMovementSpeed > minMovementSpeed)
        {
            initialMovementSpeed -= speedDecayRate * Time.deltaTime;
            initialMovementSpeed = Mathf.Max(initialMovementSpeed, minMovementSpeed); 
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    public void ProcessInputs()
    {
        
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        movementDirection = new Vector2(moveX, moveY).normalized;
    }

    public void MovePlayer()
    {
       
        if (initialMovementSpeed <= 0f)
        {
            dziadRigidbody.linearVelocity = Vector2.zero; 
            return;
        }

        
        dziadRigidbody.linearVelocity = new Vector2(movementDirection.x * initialMovementSpeed, movementDirection.y * initialMovementSpeed);
    }
}