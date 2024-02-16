using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthOffset : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public EnemyHealth EH;
    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.identity;
        transform.position = target.position + offset;
        if(EH.currentHealth <= 0)
        {
            Destroy(gameObject, 0.5f);
        }
    }
}
