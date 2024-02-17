using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLook : MonoBehaviour
{
    public Transform player;
    public bool isFlipped = false;

    public void LookatPlayer()
    {
        Vector3 Flipped = transform.localScale;
        Flipped.z *= 1f;

        if(transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = Flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if(transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = Flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }
}
