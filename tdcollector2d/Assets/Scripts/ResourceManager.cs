using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    private Dictionary<ResourceTypeSO, int> ResourceTypeDictionary;
    private ResourceTypeListSO ResourceTypeList;
    public static ResourceManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        ResourceTypeDictionary = new Dictionary<ResourceTypeSO, int>();
        ResourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
        foreach (ResourceTypeSO ResourceType in ResourceTypeList.list)
        {
            ResourceTypeDictionary[ResourceType] = 0;
        }
    }

    public void AddResources(ResourceTypeSO ResourceType, int amount)
    {
        ResourceTypeDictionary[ResourceType] = amount;
    }
}
