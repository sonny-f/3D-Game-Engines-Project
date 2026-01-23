using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float totalHealth = 100f;
    public float currentHealth;

    public Image healthBar;

    private void Awake()
    {
        currentHealth = totalHealth;
    }

    private void Update()
    {
        if(currentHealth <= 0f)
        {
            DestroyPlayer();
        }

        if(currentHealth > totalHealth)
        {
            currentHealth = totalHealth;
        }

        healthBar.fillAmount = currentHealth / totalHealth;
    }

    private void DestroyPlayer()
    {
        SceneManager.LoadScene("Death");
        Destroy(this.gameObject);
    }

    public void AddHealth(float amount)
    {
        if(currentHealth <= totalHealth)
        {
            currentHealth += amount;
        }

    }
}
