using UnityEngine;
using UnityEngine.InputSystem;


public class Milk : MonoBehaviour
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
            }
            return;
        }
        //resets the items position and velocity
        if(!Game_Manager.Instance.startGame && !isReset)
        {
            transform.position = startPosition;
            rb.gravityScale = 0;
            rb.linearVelocity = Vector2.zero;
            isReset = true;
        }

        //get mouse position
        mousePos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
        mouseWorldPos.z = 0;
        
        //get the distance of the mouse to milk
        distance = (mouseWorldPos - transform.position).sqrMagnitude;
        
        //save the offset
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            offset = transform.position - mouseWorldPos;
        }
        
        //if the mouse is over milk and left mouse button is clicked the milk will follow the mouse
        if (Mouse.current.leftButton.isPressed && distance < 0.3f)
        {
            canMove = true;
        }

        //if the player stops holding left click canMove will be set to false
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            canMove = false;
        }
        
        // allows the player to move the milk if canMove is true
        if (canMove)
        {
            // Calculate where the object SHOULD be
            Vector3 targetPosition = mouseWorldPos + offset;
        
            // Move via physics so it stops at walls
            rb.MovePosition(targetPosition);
        }
        
    }
}
