using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePoint;
    public LayerMask whatStopsMovement;
    public TextMeshProUGUI moveCounterText;

    private Vector3 inputDir = Vector3.zero;
    private int move = 0;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 lastInput = Vector2.down;

    // Save starting position
    private Vector3 startPosition;
    private Vector3 startMovePoint;


    void Start()
    {
        movePoint.parent = null;
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        UpdateMoveText();

    }

    void Update()
    {
        // Smooth move
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        // Check walking
        bool isMoving = Vector3.Distance(transform.position, movePoint.position) > 0.05f;
        animator.SetBool("isWalking", isMoving);

        // If ready to move to new tile
        if (!isMoving && inputDir != Vector3.zero)
        {
            if (!Physics2D.OverlapCircle(movePoint.position + inputDir, .4f, whatStopsMovement))
            {
                movePoint.position += inputDir;
                move++;
                UpdateMoveText();

                lastInput = inputDir; // Save last direction
            }
            inputDir = Vector3.zero;
        }

        // Feed animation parameters
        animator.SetFloat("InputX", lastInput.x);
        animator.SetFloat("InputY", lastInput.y);
        animator.SetFloat("LastInputX", lastInput.x);
        animator.SetFloat("LastInputY", lastInput.y);

        // Flip sprite only for horizontal
        if (lastInput.x > 0) spriteRenderer.flipX = true;
        else if (lastInput.x < 0) spriteRenderer.flipX = false;
    }

    // Button functions
    public void MoveUp()
    {
        inputDir = Vector3.up;
    }
    public void MoveDown()
    {
        inputDir = Vector3.down;
    }
    public void MoveLeft()
    {
        inputDir = Vector3.left;
    }
    public void MoveRight()
    {
        inputDir = Vector3.right;
    }

    void UpdateMoveText()
    {
        moveCounterText.text = "" + move;
    }
    
}