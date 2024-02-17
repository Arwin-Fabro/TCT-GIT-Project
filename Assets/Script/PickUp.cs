using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public bool Pressed = false;
    public void MUD()
    {
        Pressed = true;
        GetComponent<Rigidbody2D>().isKinematic = true;
    }
    public void MUP()
    {
        Pressed = false;
        GetComponent<Rigidbody2D>().isKinematic = false;
    }
    void Update()
    {
        if (Pressed)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos;
           
        }
        
    }
}
