using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bgchanger : MonoBehaviour
{
    
    public float level;
    public GameObject bg1;
    public GameObject bg2;
    public GameObject bg3;
    // Start is called before the first frame update
    void Start()
    {
        level = PlayerPrefs.GetFloat("level");
        if (level == 1)
        {
            bg1.SetActive(true);
        }
        if (level == 2)
        {
            bg2.SetActive(true);
        }
        if (level == 3)
        {
            bg3.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
