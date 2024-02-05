using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMap : MonoBehaviour
{
    public float rotateSpeed;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            transform.Rotate(new Vector3(0, 0, -90f));
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            transform.Rotate(new Vector3(0, 0, 90f));
        }

    }
}
