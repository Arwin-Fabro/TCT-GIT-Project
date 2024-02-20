using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public string Level = "Level";
    public int TotalKeys;
    public static int Keys;
    public bool canEnter = false;
    public bool onDoor = false;
    public Text KeyAmount;
    public Text DoorInfo;

    void Start()
    {
        TotalKeys = GameObject.FindGameObjectsWithTag("Key").Length;
    }
    void Update()
    {
        if (Keys >= TotalKeys)
        {
            canEnter = true;
            DoorInfo.color = Color.green;
            DoorInfo.text = "Door Open";

        }
        if (onDoor)
        {
            if (Input.GetKeyDown(KeyCode.E) && canEnter)
            {
                SceneManager.LoadScene(Level);
            }
        }
        KeyAmount.text = ": " + Keys + "/" + TotalKeys;

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            onDoor = true;
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            onDoor = false;
        }
    }
}
