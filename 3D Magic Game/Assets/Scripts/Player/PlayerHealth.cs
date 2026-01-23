using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;

    public Image healthBar;

    private void Start()
    {
        health = 100f;
    }

    private void Update()
    {
        if(health <= 0f)
        {
            DestroyPlayer();
        }

        if(health > 100f)
        {
            health = 100f;
        }

        healthBar.fillAmount = health / 100f;
    }

    private void DestroyPlayer()
    {
        SceneManager.LoadScene("Death");
        Destroy(this.gameObject);
    }
}
