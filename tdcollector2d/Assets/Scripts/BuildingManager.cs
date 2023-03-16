using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }
    private Camera maincam;
    public GameObject prefab;
    private BuildingTypeSO activeBuildingType;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        maincam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(activeBuildingType != null && !EventSystem.current.IsPointerOverGameObject())
            {
                Instantiate(activeBuildingType.prefab, GetMousePosition(), Quaternion.identity);

            }

        }
    }
    private Vector3 GetMousePosition()
    {
        Vector3 MousePosition = maincam.ScreenToWorldPoint(Input.mousePosition);
        MousePosition.z = 0;
        return MousePosition;
    }
    public void SetactiveBuildingType(BuildingTypeSO buildingType)
    {
        activeBuildingType = buildingType;
    }
    public BuildingTypeSO GetactiveBuildingType()
    {
        return activeBuildingType;
    }
}
