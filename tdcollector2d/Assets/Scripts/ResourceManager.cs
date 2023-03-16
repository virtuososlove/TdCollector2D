using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    private Dictionary<ResourceTypeSO, int> ResourceTypeDictionary;
    private ResourceTypeListSO ResourceTypeList;
    public event EventHandler OnResourceAmountChanged;
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
        ResourceTypeDictionary[ResourceType] += amount;
        if (OnResourceAmountChanged != null)
        {
            OnResourceAmountChanged(this, EventArgs.Empty);
        }
    }
    public int GetResourceAmount(ResourceTypeSO ResourceType)
    {
        int amount = ResourceTypeDictionary[ResourceType];
        return amount;
    }
}
