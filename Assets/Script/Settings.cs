using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public GameObject SettingCanvas;
    public GameObject SettingIcon;
    public GameObject TutorialUI;

    public void OpenSetting()
    {
        SettingCanvas.SetActive(true);
        SettingIcon.SetActive(false);
    }
    public void CloseSetting()
    {
        SettingCanvas.SetActive(false);
        SettingIcon.SetActive(true);
    }
    public void OpenTutorial()
    {
        SettingCanvas.SetActive(false);
        TutorialUI.SetActive(true);
    }
    public void CloseTutorial()
    {
        TutorialUI.SetActive(false);
        SettingCanvas.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
