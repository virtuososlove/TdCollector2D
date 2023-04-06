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
    [SerializeField] Building HQbuilding;
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
                if(canConstructBuild(UtilsClass.GetMousePosition(), activeBuildingType, out string errorString))
                {
                    if (ResourceManager.Instance.CanAffordForBuild(activeBuildingType.buildingCosts))
                    {
                        ResourceManager.Instance.DeleteResources(activeBuildingType.buildingCosts);
                        Instantiate(activeBuildingType.prefab, UtilsClass.GetMousePosition(), Quaternion.identity);
                    }
                    else
                    {
                        TooltipUI.Instance.Show("Cannot afford" + activeBuildingType.GetbuildingCostString(), new TooltipUI.ToolTipTimer { timer = 1.25f });
                    }

                }
                else
                {
                    TooltipUI.Instance.Show(errorString, new TooltipUI.ToolTipTimer { timer = 2f });
                }

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
    private bool canConstructBuild(Vector3 position, BuildingTypeSO buildingType, out string ErrorString)
    {
        BoxCollider2D boxCollider2D = buildingType.prefab.GetComponent<BoxCollider2D>();
        Collider2D[] colliders = Physics2D.OverlapBoxAll(position, boxCollider2D.size,0);
        if(colliders.Length != 0)
        {
            ErrorString = "Area is not Clear!";
            return false;
        }
        colliders = Physics2D.OverlapCircleAll(position, activeBuildingType.ResourceGeneratorData.canConstractScanZone);

        foreach ( Collider2D collider in colliders)
        {
            if(collider.GetComponent<BuildingTypeHolder>() != null)
            {
                if (buildingType == collider.GetComponent<BuildingTypeHolder>().buildingType)
                {
                    ErrorString = "Too close to the same type of building!";
                    return false;
                }
            }
        }
        float maxConstructionRadius = 25;
        colliders = Physics2D.OverlapCircleAll(position, maxConstructionRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.GetComponent<BuildingTypeHolder>() != null)
            {
                ErrorString = "";
                return true; 
            }
        }
        ErrorString = "Too away from any other buildings!";
        return false;
    }
    
    public Building GetHQBuilding()
    {
        return HQbuilding;
    }
}   
