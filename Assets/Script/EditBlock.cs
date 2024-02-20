using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditBlock : MonoBehaviour
{
    public bool ActivateBlock = false;
    public bool DisableBlock = false;
    public bool RotateCW = false;
    public bool RotateACW = false;
    public GameObject TheBlock;

    public void OnCollisionEnter2D(Collision2D block)
    {
        if (block.gameObject.tag == "Box")
        {
            print("Triggered");
            if (ActivateBlock == true)
            {
                print("Activate");
                TheBlock.SetActive(true);
            }
            if (DisableBlock == true)
            {
                TheBlock.SetActive(false);
            }
            if (RotateCW == true)
            {
                TheBlock.transform.Rotate(0f, 0f, 90f);
            }
            if (RotateACW == true)
            {
                TheBlock.transform.Rotate(0f, 0f, -90f);
            }
        }
    }

}
