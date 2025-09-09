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

    void Start()
    {
        movePoint.parent = null;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f)
        {
            if (inputDir != Vector3.zero)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + inputDir, .4f, whatStopsMovement))
                {
                    movePoint.position += inputDir;
                    move++;
                    UpdateMoveText();
                }
                inputDir = Vector3.zero; // reset so it only moves once per button press
            }
        }
    }

    // Button functions
    public void MoveUp()    
    { 
        inputDir = Vector3.up; 
        RotatePlayer(inputDir.x, inputDir.y); 
    }
    public void MoveDown()  
    { 
        inputDir = Vector3.down; 
        RotatePlayer(inputDir.x, inputDir.y); 
    }
    public void MoveLeft()  
    { 
        inputDir = Vector3.left; 
        RotatePlayer(inputDir.x, inputDir.y); 
    }
    public void MoveRight() 
    { 
        inputDir = Vector3.right; 
        RotatePlayer(inputDir.x, inputDir.y); 
    }

    void RotatePlayer(float x, float y)
    {
        if (x == 0 && y == 0) return; // no rotation if no movement

        float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    void UpdateMoveText()
    {
        moveCounterText.text = "" + move;
    }
}