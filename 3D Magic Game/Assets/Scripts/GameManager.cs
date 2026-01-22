using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
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
}
