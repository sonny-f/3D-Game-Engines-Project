using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;

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
    }

    private void DestroyPlayer()
    {
        Destroy(this.gameObject);
    }
}
