using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingLayerController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private int extender = 5;
    public float addExtraControl = 0;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = -(int)(transform.position.y + addExtraControl )* extender;
    }

    
    void Update()
    {
        
    }
}
