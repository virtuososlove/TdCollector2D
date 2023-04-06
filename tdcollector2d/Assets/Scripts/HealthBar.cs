using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Transform healthBar;
    [SerializeField] HealthSystem healthSystem;
    private void Awake()
    {
        healthBar = transform.Find("Bar");

    }
    void Start()
    {
        healthSystem.onDamaged += HealthSystem_onDamaged;
        UpdateBar();
        UpdateBarActive();
    }

    private void HealthSystem_onDamaged(object sender, System.EventArgs e)
    {
        UpdateBar();
        UpdateBarActive();
    }

    private void UpdateBar()
    {
        healthBar.localScale = new Vector3(healthSystem.GetHealthNormalized(), 1, 1);
    }
    private void UpdateBarActive()
    {
        if (healthSystem.GetHealthNormalized() == 1)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
