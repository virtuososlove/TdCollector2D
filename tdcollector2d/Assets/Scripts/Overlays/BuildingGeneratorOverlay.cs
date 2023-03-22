using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildingGeneratorOverlay : MonoBehaviour
{
    private BuildingTypeSO buildingType;
    private ResourceGeneratorData resourceGeneratorData;
    private ResourceGenerator resourceGenerator;
    void Start()
    {
        buildingType = transform.parent.GetComponent<BuildingTypeHolder>().buildingType;
        resourceGenerator = transform.parent.GetComponent<ResourceGenerator>();
        resourceGeneratorData = buildingType.ResourceGeneratorData;
        transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.ResourceType.Sprite;
        transform.Find("Text").GetComponent<TextMeshPro>().SetText(resourceGenerator.GetAmountGeneratedValuePerSecond(resourceGeneratorData,transform).ToString("F1"));
    }

    void Update()
    {
        if (resourceGenerator.isActiveAndEnabled)
        {
            transform.Find("Bar").localScale = new Vector3(1- resourceGenerator.getTimerNormalized(),1,1);
        }
    }
}
