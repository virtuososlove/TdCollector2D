using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    void Start()
    {
        BuildingManager.Instance.onActiveBuildingTypeChanged += BuildingManager_onActiveBuildingTypeChanged;

    }
    private void Awake()
    {
        Hide();

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
        } 
    }

    void Update()
    {
        transform.position = UtilsClass.GetMousePosition();
    }
    private void Show(Sprite sprite)
    {
        gameObject.SetActive(true);
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
