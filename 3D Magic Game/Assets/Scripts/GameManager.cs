using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Camera mainCam;
    public Camera overlayCam;
    public GameObject settingsMenu;
    public GameObject pauseMenu;
    public TMP_Dropdown graphicsDropdown;

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void Play()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void Quit()
    {
        Application.Quit();
    }   

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void SettingsMenu()
    {
        settingsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        if (settingsMenu.activeSelf || pauseMenu.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }

        if(SceneManager.GetActiveScene().name == "End" || SceneManager.GetActiveScene().name == "Death")
        {
            pauseMenu.SetActive(false);
            settingsMenu.SetActive(false);
        }
        else if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            {
                pauseMenu.SetActive(false);
            }
        }

        if(Time.timeScale ==1f)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void BackButton()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }
}
