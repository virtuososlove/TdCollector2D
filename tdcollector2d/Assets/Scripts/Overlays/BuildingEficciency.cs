using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BuildingEficciency : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private ResourceGeneratorData resourceGeneratorData;
    private TextMeshPro Text;
    void Start()
    {
        spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        Text = transform.Find("Text").GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        int numberOfResourceNode = ResourceGenerator.CalculateNumberOfResourceNode(resourceGeneratorData, transform.parent.transform);
        float percent = Mathf.RoundToInt((float) numberOfResourceNode / resourceGeneratorData.maxResourceNodeCounter * 100f);

        Text.SetText(percent + "%");
    }
    public void show(ResourceGeneratorData resourceGeneratorData)
    {
        this.resourceGeneratorData = resourceGeneratorData;

        spriteRenderer.sprite = resourceGeneratorData.ResourceType.Sprite;
    }
}
