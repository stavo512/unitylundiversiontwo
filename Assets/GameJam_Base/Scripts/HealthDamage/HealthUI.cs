using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    //requires UI slider without Handle object. 

    public Health targetHealth; //Object with the target Health Script attached. 
    public Slider healthSlider;

    void Start()
    {
        if (targetHealth == null || healthSlider == null)
        {
            Debug.LogError("HealthUI: Missing reference");
            enabled = false;
            return;
        }

        healthSlider.maxValue = targetHealth.maxHealth;
        healthSlider.value = targetHealth.Current;
    }

    void Update()
    {
        healthSlider.value = targetHealth.Current;
    }
}
