using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformactivate : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject trigger;
    public GameObject child;
    void Start()
    {
        child= trigger.transform.GetChild(0).gameObject;
        //child.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag=="activate")
        {            
            child.SetActive(true);
        }
        if (other.gameObject.tag == "deactivate")
        {
            child.SetActive(false);
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "activate")
        {            
            child.SetActive(false);
        }
        if(other.gameObject.tag == "deactivate")
        {            
            child.SetActive(true);
        }
    }
}
