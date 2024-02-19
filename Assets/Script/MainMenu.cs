using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject Title;
    public GameObject Buttons;
    public GameObject Map;
    public bool hasClicked = false;
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && hasClicked == false)
        {
            Title.SetActive(false);
            Buttons.SetActive(true);
            hasClicked = true;

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseMap();
        }
    }
    public void OpenMap()
    {
        Map.SetActive(true);
        Buttons.SetActive(false);
    }
    public void CloseMap()
    {
        Map.SetActive(false);
        Buttons.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
