using Unity.VisualScripting;
using UnityEngine;

public class Button_press : MonoBehaviour
{
    public bool pressed = false;

    [SerializeField] private DoorActivated activate;
    
    
    void Start()
    {
        
    }
    //When cat is on the button pressed = true
    private void OnTriggerEnter2D(Collider2D Cat)
    {
        if (Cat.CompareTag("Cat"))
        {
            pressed = true;
        }
            
    }
    //When cat gets off the button pressed = false
    private void OnTriggerExit2D(Collider2D Cat)
    {
        if (Cat.CompareTag("Cat"))
        {
            pressed = false;
        }
    }
    
    void Update()
    {
        activate.isOpen = pressed;
    }
    
}
