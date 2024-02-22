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
    public Character CharacterScript;

    void Start()
    {
        Keys = 0;
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
            else if (Input.GetKeyDown(KeyCode.E) && !canEnter)
            {
                CharacterScript.infoText.text = "Not Enough Keys!";
                CharacterScript.infoTxt.SetActive(true);
                Invoke("CloseText", 5f);
            }
        }
        KeyAmount.text = ": " + Keys + "/" + TotalKeys;

    }
    public void CloseText()
    {
        CharacterScript.infoTxt.SetActive(false);
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
