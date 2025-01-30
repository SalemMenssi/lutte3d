using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;

    }

    public void SetHealth(float health)
    {
        StartCoroutine(SmoothSetHealth(health));
    }

    private IEnumerator SmoothSetHealth(float targetHealth)
    {
        float startHealth = slider.value;
        float elapsedTime = 0f;
        float duration = 0.2f; 

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            slider.value = Mathf.Lerp(startHealth, targetHealth, elapsedTime / duration);
            yield return null; 
        }

        
        slider.value = targetHealth;
    }
}
