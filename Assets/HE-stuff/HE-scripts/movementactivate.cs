using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementactivate : MonoBehaviour
{
    public List<Transform> waypoint;
    public int count;    
    public float ms;
    public int target;
    public bool canmove=false;
    public bool repeatable;
    // Start is called before the first frame update
    void Start()
    { 
    count = waypoint.Count-1;
    }
    void Update()
    {
        
        if (canmove==true)
        { 
        transform.position = Vector3.MoveTowards(transform.position,waypoint[target].position,ms*Time.deltaTime);
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position==waypoint[target].position)
        {
            if (target == waypoint.Count - 1)
            {
                if (repeatable == true)
                {
                    target = 0;
                }
                else 
                {
                 target = count;
                }                
               
            }
            else 
            {
                target += 1;
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag=="waypoint")
        {
            canmove = false;
        }
        
    }
    void OnTriggerStay2D(Collider2D other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            canmove = true;
        }
    }
}
