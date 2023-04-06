using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    private float TimerMax;
    private float timer;
    private BuildingTypeSO buildingType;
    private int resourceNodeCounter;
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

        resourceNodeCounter = CalculateNumberOfResourceNode(resourceGeneratorData, transform);
        if (resourceNodeCounter == 0)
        {
            TimerMax = 0;
            enabled = false;
        }
        else
        {
            TimerMax = resourceGeneratorData.TimerMax / 2 + resourceGeneratorData.TimerMax * (1 - (float)resourceNodeCounter / maxResourceNodeCounter);
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
    public float getTimerNormalized()
    {
        return timer / TimerMax;
    }
    public float GetAmountGeneratedValuePerSecond(ResourceGeneratorData resourceGeneratorData, Transform transform)
    {
        //float cloneTimerMax = resourceGeneratorData.TimerMax;
        //float resourceNodeCounterclone = CalculateNumberOfResourceNode(resourceGeneratorData, transform);
      
        //cloneTimerMax = resourceGeneratorData.TimerMax / 2 + resourceGeneratorData.TimerMax * (1 - (float)resourceNodeCounterclone / resourceGeneratorData.maxResourceNodeCounter);
        return 1 / TimerMax;
        
    }
    public static int CalculateNumberOfResourceNode(ResourceGeneratorData resourceGeneratorData,Transform transform)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, resourceGeneratorData.maxRadius);
        int resourceNodeCount = 0;

        foreach (Collider2D collider in colliders)
        {
            ResourceNode resourceNode = collider.GetComponent<ResourceNode>();
            if (resourceNode != null)
            {
                if (resourceNode.resourceTypeSO == resourceGeneratorData.ResourceType)
                {
                    resourceNodeCount++;
                }
            }
        }
        resourceNodeCount = Mathf.Clamp(resourceNodeCount, 0, resourceGeneratorData.maxResourceNodeCounter);
        return resourceNodeCount;
    }
}
