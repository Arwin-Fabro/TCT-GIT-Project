using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class bgintsetter : MonoBehaviour
{
    // Start is called before the first frame update
    public Scene scene;
    public float level;
    void Start()
    {

        scene = SceneManager.GetActiveScene();

        level = PlayerPrefs.GetFloat("level");
        if (scene.name == "Level1")
        {
            PlayerPrefs.SetFloat("level", 1);
        }
        if (scene.name == "Level2")
        {
            PlayerPrefs.SetFloat("level", 2);
        }
        if (scene.name == "Level3")
        {
            PlayerPrefs.SetFloat("level", 3);
        }
        else
        {
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
