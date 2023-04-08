using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BuildingSelectUI : MonoBehaviour
{
    private Transform BuildingSelectBtnTemp;
    private BuildingTypeListSO buildingTypeList;
    public Transform ArrowButtonTMP;
    private Dictionary<BuildingTypeSO, Transform> BuildingButtonTransformDict;
    int Amountindex = 1;
    public List<BuildingTypeSO> dontCareTypeList;
    private TooltipUI tooltipUI;
    private void Awake()
    {
        tooltipUI = TooltipUI.Instance;
        ArrowButtonTMP.GetComponent<Button>().onClick.AddListener(() => {
            BuildingManager.Instance.SetactiveBuildingType(null);
        });

        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
        BuildingButtonTransformDict = new Dictionary<BuildingTypeSO, Transform>();
       
        BuildingSelectBtnTemp = transform.Find("BuildingButtonTemplate");
        BuildingSelectBtnTemp.gameObject.SetActive(false); 
        foreach (BuildingTypeSO buildingType in buildingTypeList.list)
        {
            if (dontCareTypeList.Contains(buildingType))continue;
            
            Transform buildingButton = Instantiate(BuildingSelectBtnTemp, transform);
            buildingButton.gameObject.SetActive(true);
            BuildingButtonTransformDict[buildingType] = buildingButton;

            float offsetAmount = 80;
            buildingButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * Amountindex,0);
            buildingButton.Find("Image").GetComponent<Image>().sprite = buildingType.sprite;
            buildingButton.GetComponent<Button>().onClick.AddListener(() => {
                BuildingManager.Instance.SetactiveBuildingType(buildingType);
            });

            Amountindex++;

            buildingButton.GetComponent<OnMouseEnterExit>().onMouseEnter += (object sender, EventArgs e) => {
                tooltipUI.Show(buildingType.Namestring + "\n" + buildingType.GetbuildingCostString());
            };

            buildingButton.GetComponent<OnMouseEnterExit>().onMouseExit += (object sender, EventArgs e) =>
            {
                tooltipUI.Hide();
            };
        }
    }

   

    

    private void Start()
    {
        BuildingManager.Instance.onActiveBuildingTypeChanged += BuildingManager_onActiveBuildingTypeChanged;
    }

    private void BuildingManager_onActiveBuildingTypeChanged(object sender, BuildingManager.onActiveBuildingTypeChangedEventArgs e)
    {
        UpdateActiveBuildingType();
    }

    private void UpdateActiveBuildingType()
    {
        ArrowButtonTMP.Find("Outline").gameObject.SetActive(false);
        foreach (BuildingTypeSO buildingType in BuildingButtonTransformDict.Keys)
        {
            BuildingButtonTransformDict[buildingType].Find("Outline").gameObject.SetActive(false);
        }
        BuildingTypeSO activeBuildingType = BuildingManager.Instance.GetactiveBuildingType();
        if (activeBuildingType == null)
        {
            ArrowButtonTMP.Find("Outline").gameObject.SetActive(true);
        }
        else
        {
            BuildingButtonTransformDict[activeBuildingType].Find("Outline").gameObject.SetActive(true);
        }

    }
}
