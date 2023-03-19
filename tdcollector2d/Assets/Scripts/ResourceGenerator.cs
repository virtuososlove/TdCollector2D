using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    private float TimerMax;
    private float timer = 0.5f;
    private BuildingTypeSO buildingType;
    public int resourceNodeCounter = 0;
    private ResourceGeneratorData resourceGeneratorData;
    private int maxResourceNodeCounter;
    private void Awake()
    {
        buildingType = GetComponent<BuildingTypeHolder>().buildingType;
        resourceGeneratorData = buildingType.ResourceGeneratorData;
        TimerMax = resourceGeneratorData.TimerMax;
        maxResourceNodeCounter = resourceGeneratorData.maxResourceNodeCounter;

    }
    void Start()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 7);
        foreach (Collider2D collider in colliders)
        {
            ResourceNode resourceNode = collider.GetComponent<ResourceNode>();
            if (resourceNode != null)
            {
                if(resourceNode.resourceTypeSO == resourceGeneratorData.ResourceType)
                {
                    resourceNodeCounter++;
                }
            }
        }
        resourceNodeCounter = Mathf.Clamp(resourceNodeCounter, 0, maxResourceNodeCounter);
        if (resourceNodeCounter == 0)
        {
            enabled = false;
        }
        else
        {
            TimerMax = TimerMax / 2 + TimerMax * (1 - resourceNodeCounter / maxResourceNodeCounter);
        }


    }

    void Update()
    {
        

        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            timer = TimerMax;
            ResourceManager.Instance.AddResources(resourceGeneratorData.ResourceType, resourceGeneratorData.amount);
            
        }
    }
    private void DetectAroundResources()
    {
        
    }
}
