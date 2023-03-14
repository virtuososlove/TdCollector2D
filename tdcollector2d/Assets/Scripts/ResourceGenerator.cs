using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    [SerializeField] float TimerMax;
    private float timer;
    private BuildingTypeSO buildingType;

    void Start()
    {
        timer = TimerMax;
        buildingType = GetComponent<BuildingTypeHolder>().buildingType;

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            timer = TimerMax;
            ResourceManager.Instance.AddResources(buildingType.ResourceGeneratorData.ResourceType, buildingType.ResourceGeneratorData.amount);
        }
    }
}
