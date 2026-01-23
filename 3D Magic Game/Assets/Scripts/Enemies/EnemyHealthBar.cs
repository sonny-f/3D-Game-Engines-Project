using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Image enemyHealthBar;

    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        enemyHealthBar.fillAmount = currentValue / maxValue;
    }
}
