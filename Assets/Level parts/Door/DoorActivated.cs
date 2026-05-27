using UnityEngine;

public class DoorActivated : MonoBehaviour
{
    public float openAngle = 90f; // Degrees to rotate
    public float smoothSpeed = 2f;
    public bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        // Store initial rotation as closed state
        closedRotation = transform.rotation;
        // Calculate open rotation (rotating around Z-axis for 2D)
        openRotation = Quaternion.Euler(0, 0, openAngle);
    }

    void Update()
    {
        // Determine target rotation based on state
        Quaternion targetRotation = isOpen ? openRotation : closedRotation;
        // Smoothly rotate towards the target
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smoothSpeed);
    }
   
}

