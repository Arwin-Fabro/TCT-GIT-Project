using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public bool Pressed = false;
    private Transform dragging = null;
    private Vector3 offset;
    public void MUD()
    {
        Pressed = true;
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, float.PositiveInfinity, LayerMask.GetMask("Movable"));
        if(hit)
        {
            dragging = hit.transform;
            offset = dragging.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
    public void MUP()
    {
        Pressed = false;
        dragging = null;
    }
    void Update()
    {
        if(dragging != null && Pressed)
        {
            dragging.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }
        
    }
}
