using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class OnMouseEnterExit : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public event EventHandler onMouseExit;
    public event EventHandler onMouseEnter;
     

    public void OnPointerEnter(PointerEventData eventData)
    {
       if(onMouseEnter != null)
        {
            onMouseEnter(this, EventArgs.Empty);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (onMouseExit != null)
        {
            onMouseExit(this, EventArgs.Empty);
        }
    }

  
}
