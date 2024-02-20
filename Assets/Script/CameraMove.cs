using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float speed = 4;
    public float startPos;

    void Start()
    {
        startPos = transform.position.x;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += transform.right * Time.deltaTime * speed ; //Camera Moving right
        if (transform.position.x > 100)
        {
            transform.position = new Vector3(startPos, transform.position.y, transform.position.z);
        }
    }
}
