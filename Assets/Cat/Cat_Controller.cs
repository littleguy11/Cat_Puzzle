using UnityEngine;

public class Cat_Controller : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private GameObject target;
    
    [SerializeField] float acceleration = 10f;
    [SerializeField] float drag = 2f;
    [SerializeField] Rigidbody2D rb;
        
    
    void Start()
    {
        target = FindClosestByTag();
        
        rb = GetComponent<Rigidbody2D>();
        rb.linearDamping = drag;
    }
    
    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            
            rb.AddForce(direction * acceleration);
        }
    }

    public GameObject FindClosestByTag()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Item");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.magnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}
