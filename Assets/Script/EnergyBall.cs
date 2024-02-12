using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBall : MonoBehaviour
{
    public float speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 0f, speed * Time.deltaTime);
        transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        Destroy(this.gameObject, 1.5f);

    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Arrows")
        {
            Destroy(other.gameObject);
        }
    }
}
