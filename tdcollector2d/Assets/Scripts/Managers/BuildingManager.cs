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
            if(activeBuildingType != null && !EventSystem.current.IsPointerOverGameObject() && canConstructBuild(UtilsClass.GetMousePosition(), activeBuildingType))
            {
                if (ResourceManager.Instance.CanAffordForBuild(activeBuildingType.buildingCosts))
                {
                    ResourceManager.Instance.DeleteResources(activeBuildingType.buildingCosts);
                    Instantiate(activeBuildingType.prefab, UtilsClass.GetMousePosition(), Quaternion.identity);

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
    private bool canConstructBuild(Vector3 position, BuildingTypeSO buildingType)
    {
        BoxCollider2D boxCollider2D = buildingType.prefab.GetComponent<BoxCollider2D>();
        Collider2D[] colliders = Physics2D.OverlapBoxAll(position, boxCollider2D.size,0);
        if(colliders.Length != 0)
        {
            return false;
        }
        colliders = Physics2D.OverlapCircleAll(position, activeBuildingType.ResourceGeneratorData.canConstractScanZone);

        foreach ( Collider2D collider in colliders)
        {
            if(collider.GetComponent<BuildingTypeHolder>() != null)
            {
                if (buildingType == collider.GetComponent<BuildingTypeHolder>().buildingType)
                {
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
                return true; 
            }
        }
        return false;
    }
    
}   
