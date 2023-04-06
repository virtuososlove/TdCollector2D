using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilsClass
{
    public static Camera MainCamera;
    public static Vector3 GetMousePosition()
    {
        if(MainCamera == null)
        {
            MainCamera = Camera.main;
        }
        Vector3 MousePosition = MainCamera.ScreenToWorldPoint(Input.mousePosition);
        MousePosition.z = 0;
        return MousePosition;
    }
    public static Vector3 GetRandomDir()
    {
        return new Vector3(Random.Range(-1, 1), Random.Range(-1, 1)).normalized;
    }
}
