using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Camera mainCam;
    public Camera overlayCam;
    public GameObject settingsMenu;
    public GameObject pauseMenu;

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

    public void ApplyPost(bool toggleValue)
    {
        UnityEngine.Rendering.Universal.UniversalAdditionalCameraData uac = gameObject.GetComponent<UnityEngine.Rendering.Universal.UniversalAdditionalCameraData>();
        if(toggleValue == false)
            uac.renderPostProcessing = false;
        else
            uac.renderPostProcessing = true;
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
    }
}
