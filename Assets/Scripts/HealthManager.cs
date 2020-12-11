using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Slider healthbar;
    public FloatValue currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        healthbar = GetComponent<Slider>();
        currentHealth.initialValue = healthbar.maxValue;
    }

    public void ChangeHP()
    {
        healthbar.value = currentHealth.runtimeValue;
    }
}
