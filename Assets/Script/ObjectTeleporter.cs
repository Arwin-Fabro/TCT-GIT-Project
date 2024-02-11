using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTeleporter : MonoBehaviour
{
    private GameObject currentTeleporter;


    void Update()
    {

            if (currentTeleporter != null)
            {
                transform.position = currentTeleporter.GetComponent<Portal>().GetDestination().position;
            }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Portal")
        {
            currentTeleporter = other.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == currentTeleporter)
        {
            currentTeleporter = null;
        }
    }
}
