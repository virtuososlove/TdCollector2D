using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/BuildingTypeSO")]
public class BuildingTypeSO : ScriptableObject
{
    public string Namestring;
    public GameObject prefab;
    public bool canGenerateResource;
    public ResourceGeneratorData ResourceGeneratorData;
    public Sprite sprite;
    public float MaxHealthAmount;
    public BuildingCosts[] buildingCosts;
    public string GetbuildingCostString()
    {
        string str = "";
        foreach(BuildingCosts buildingCost in buildingCosts)
        {
            str += buildingCost.resourceType.nameString + ": " + buildingCost.cost.ToString();
        }
       
        return str;
    }
}
