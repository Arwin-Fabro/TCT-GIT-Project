using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public float level;
    void Start()
    {
        level = PlayerPrefs.GetFloat("level");
    }
    public void Level1()
    {
        SceneManager.LoadScene("Level1");
    }
    public void Level2()
    {
        SceneManager.LoadScene("Level2");
    }
    public void Level3()
    {
        SceneManager.LoadScene("Level3");
    }
    public void mainmenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void R()
    {
        if (level==1)
        {
            Level1();
        }
        if (level==2)
        {
            Level2();
        }
        if (level == 3)
        {
            Level3();
        }
        else
        {
            print("help");
        }
    }
}
