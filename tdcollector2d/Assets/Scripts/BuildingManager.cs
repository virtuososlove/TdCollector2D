using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }
    public GameObject prefab;
    private BuildingTypeSO activeBuildingType;
    public event EventHandler<onActiveBuildingTypeChangedEventArgs> onActiveBuildingTypeChanged;
    public class onActiveBuildingTypeChangedEventArgs : EventArgs
    {
        public BuildingTypeSO activeBuildingType;
    }
    private void Awake()
    {
        Instance = this;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(activeBuildingType != null && !EventSystem.current.IsPointerOverGameObject())
            {
                Instantiate(activeBuildingType.prefab, UtilsClass.GetMousePosition(), Quaternion.identity);

            }

        }
    }
    
    public void SetactiveBuildingType(BuildingTypeSO buildingType)
    {
        activeBuildingType = buildingType;
        onActiveBuildingTypeChanged(this, new onActiveBuildingTypeChangedEventArgs() { activeBuildingType = activeBuildingType});
    }
    public BuildingTypeSO GetactiveBuildingType()
    {
        return activeBuildingType;
    }
}
