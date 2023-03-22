using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    private BuildingEficciency buildingEficciency;
    private GameObject spriteGameObject;
    void Start()
    {
        BuildingManager.Instance.onActiveBuildingTypeChanged += BuildingManager_onActiveBuildingTypeChanged;
        spriteGameObject = transform.Find("Sprite").gameObject;
        buildingEficciency = transform.Find("BuildingEfficency").GetComponent<BuildingEficciency>();
        Hide();

    }
    private void Awake()
    {

    }

    private void BuildingManager_onActiveBuildingTypeChanged(object sender, BuildingManager.onActiveBuildingTypeChangedEventArgs e)
    {
        if( e.activeBuildingType == null)
        {
            Hide();
        }
        else
        {
            Show(e.activeBuildingType.sprite);
            buildingEficciency.show(e.activeBuildingType.ResourceGeneratorData);
        } 
    }

    void Update()
    {
        transform.position = UtilsClass.GetMousePosition();
    }
    private void Show(Sprite sprite)
    {
        gameObject.SetActive(true);
        spriteGameObject.GetComponent<SpriteRenderer>().sprite = sprite;
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
