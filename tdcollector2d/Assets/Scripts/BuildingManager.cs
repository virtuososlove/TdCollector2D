using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    private Camera maincam;
    private BuildingTypeListSO buildingTypeList;
    public GameObject prefab;
    private void Awake()
    {
        
    }
    void Start()
    {
        maincam = Camera.main;
        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(buildingTypeList.list[0].prefab, GetMousePosition(), Quaternion.identity);
            
        }
    }
    private Vector3 GetMousePosition()
    {
        Vector3 MousePosition = maincam.ScreenToWorldPoint(Input.mousePosition);
        MousePosition.z = 0;
        return MousePosition;
    }
}
