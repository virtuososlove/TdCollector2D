using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/BuildingTypeSO")]
public class BuildingTypeSO : ScriptableObject
{
    public string Namestring;
    public GameObject prefab;
    public ResourceGeneratorData ResourceGeneratorData;
    public Sprite sprite;
    public BuildingCosts[] buildingCosts;
}
