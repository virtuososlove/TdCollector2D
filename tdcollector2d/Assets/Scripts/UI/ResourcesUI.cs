using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourcesUI : MonoBehaviour
{
    private ResourceTypeListSO ResourceTypeList;
    private Dictionary<ResourceTypeSO, Transform> ResourceTransformDictionary;
    int Amountindex = 0;
    
    private void Awake()
    {
        ResourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
        ResourceTransformDictionary = new Dictionary<ResourceTypeSO, Transform>();
        
        Transform ResourceTemplate = transform.Find("ResourceTemplate");
        ResourceTemplate.gameObject.SetActive(false);
        foreach (ResourceTypeSO ResourceType in ResourceTypeList.list)
        {
            
            Transform resourceTransform = Instantiate(ResourceTemplate, transform);
            resourceTransform.gameObject.SetActive(true);
            ResourceTransformDictionary[ResourceType] = resourceTransform;
            
            resourceTransform.Find("Image").GetComponent<Image>().sprite = ResourceType.Sprite;
            resourceTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(-120 * Amountindex, 0);
            
            int resourceAmount = ResourceManager.Instance.GetResourceAmount(ResourceType);
            resourceTransform.Find("Text").GetComponent<TextMeshProUGUI>().SetText(resourceAmount.ToString());
            
            Amountindex++;
        }
    }
    public void Start()
    {
        ResourceManager.Instance.OnResourceAmountChanged += ResourceManager_OnResourceAmountChanged;
    }
    private void ResourceManager_OnResourceAmountChanged(object sender, System.EventArgs e)
    {
        UpdateResourceText();
    }

    public void UpdateResourceText()
    {
        foreach (ResourceTypeSO ResourceType in ResourceTypeList.list)
        {
            int resourceAmount = ResourceManager.Instance.GetResourceAmount(ResourceType);
            ResourceTransformDictionary[ResourceType].Find("Text").GetComponent<TextMeshProUGUI>().SetText(resourceAmount.ToString());
        }
    }

}
