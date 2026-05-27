using UnityEngine;
using UnityEngine.InputSystem;


public class ItemMovement : MonoBehaviour
{
private Vector3 mousePos;
private Vector3 offset;
private float distance;


private Vector3 startPosition;
private bool isReset = false;
private bool canMove = false;

private Rigidbody2D rb;

    void Awake()
    {
        //sets start position -TEMP 
        startPosition = transform.position;
        
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        //stop everything if the game has started
        if (Game_Manager.Instance.startGame)
        {
            //saves the position of the item when the game starts
            if (isReset)
            {
                startPosition = transform.position;
                rb.gravityScale = 1;
                isReset = false;
                rb.simulated = true;
            }
            return;
        }
        //resets the item's position and velocity
        if(!Game_Manager.Instance.startGame && !isReset)
        {
            transform.position = startPosition;
            rb.gravityScale = 0;
            rb.linearVelocity = Vector2.zero;
            isReset = true;
            rb.simulated = false;
        }

        //get mouse position
        mousePos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
        mouseWorldPos.z = 0;
        
        //get the distance of the mouse to item
        distance = (mouseWorldPos - transform.position).sqrMagnitude;
        
        //save the offset
        if (Mouse.current.leftButton.wasPressedThisFrame && distance < 0.3f)
        {
            rb.simulated = true;
            canMove = true;
            offset = transform.position - mouseWorldPos;
        }

        //if the player stops holding left click canMove will be set to false
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            canMove = false;
            rb.simulated = false;
        }
        
        // allows the player to move the item if canMove is true
        if (canMove)
        {
            // Calculate where the object SHOULD be
            Vector3 targetPosition = mouseWorldPos + offset;
        
            // Move via physics so it stops at walls
            rb.MovePosition(targetPosition);
            
            //allow the player to move the object
            
        }
        else
        {
            //Stop it from floating away while editing the level
            
            rb.linearVelocity = Vector2.zero;
        }
        
    }
}
