using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public Camera mainCam;
    public Camera overlayCam;
    public GameObject settingsMenu;
    public GameObject pauseMenu;
    public TMP_Dropdown graphicsDropdown;
    public GameObject controlsMenu;
    public AudioMixer audioMixer;

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void VolumeSlider(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    public void Play()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void ControlsMenu()
    {
        controlsMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void BackToSettings()
    {
        controlsMenu.SetActive(false);
        settingsMenu.SetActive(true);
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
        if (settingsMenu.activeSelf || pauseMenu.activeSelf || controlsMenu.activeSelf)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
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

        if(SceneManager.GetActiveScene().name == "MainMenu" || SceneManager.GetActiveScene().name == "End" || SceneManager.GetActiveScene().name == "Death")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void BackButton()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void BackButtonMainMenu()
    {
        settingsMenu.SetActive(false);
    }
}
