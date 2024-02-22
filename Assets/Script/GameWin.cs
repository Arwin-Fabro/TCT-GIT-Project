using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWin : MonoBehaviour
{
    public GameObject EndCredit;
    public int speed;
    public bool canScroll = false;

    void Start()
    {
        StartCoroutine(StartScrolling());
    }
    void Update()
    {
        if (canScroll)
        {
            EndCredit.transform.position += transform.up * speed * Time.deltaTime;
        }

    }
    IEnumerator StartScrolling()
    {
        yield return new WaitForSeconds(3f);
        canScroll = true;

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "EndScroll")
        {
            print("Collided");
            canScroll = false;
        }
    }

}
