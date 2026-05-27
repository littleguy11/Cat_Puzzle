using System;
using UnityEngine;

public class Cat_Controller : MonoBehaviour
{
    private GameObject target = null;
    
    [SerializeField] float acceleration = 10f;
    [SerializeField] float drag = 2f;
    
    [SerializeField] private LayerMask layerMask;
    
    Rigidbody2D rb;
    SpriteRenderer sr;
    
    private Vector3 startingPosition = Vector3.zero;
    
    //bool startGame = false;

    private void Awake()
    {
        //Set starting position the position of the cat
        startingPosition = transform.position;
    }

    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        rb.linearDamping = drag;
        
        sr = GetComponent<SpriteRenderer>();
    }
    
    void FixedUpdate()
    {

        //Set startGame to the bool in the Game_Manager script
        
        if (Game_Manager.Instance.startGame)
        {
            //find the closest target
            if (target == null)
            {
                target = FindClosestByTag();
            }
        }
        else
        {
            //placeholder for animations
            sr.color = Color.purple;
             
            //reset position and target
            transform.position = startingPosition;
            target = null;
        }
        
        //If the target is found and if the game has started move to the target 
        if (target != null && Game_Manager.Instance.startGame)
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            direction.y = 0;

            if (target.transform.name.Contains("Milk"))
            {
                rb.AddForce(direction * acceleration);
            }
            else if (target.transform.name.Contains("Catnip"))
            {
                rb.AddForce(direction * acceleration * 4);
            }
            
            
            //placeholder for animations
            sr.color = Color.red;
        }
    }

    //Find the closest object with the tag "Item"
    public GameObject FindClosestByTag()
    {
        //make a list of all the GameObjects with the tag "Item"
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Item");
        
        //making all the variables needed
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        //goes through all the GameObjects in the list and finds the closest one that is visible
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.magnitude;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, diff,distance,layerMask);
            if (curDistance < distance && hit.transform.tag == "Item")
            {
                closest = go;
                distance = curDistance;
            }
        }
        //Returns the closest "Item"
        return closest;
    }
    

}
