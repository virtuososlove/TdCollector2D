using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private HealthSystem healthSystem;
    private BuildingTypeSO buildingTypeSO;
    private void Awake()
    {
        buildingTypeSO = GetComponent<BuildingTypeHolder>().buildingType;
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.setHealthAmountMax(buildingTypeSO.MaxHealthAmount, true);
    }
    void Start()
    {
        healthSystem.onDied += HealthSystem_onDied;
    }

    private void HealthSystem_onDied(object sender, System.EventArgs e)
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
