using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraHangler : MonoBehaviour
{
    
    [SerializeField] float moveSpeed;
    public CinemachineVirtualCamera CMVcam;
    [SerializeField] float scrollSpeed;
    private float currentOrthographicSize;
    private float OrthographicSize;
    void Start()
    {
        OrthographicSize = CMVcam.m_Lens.OrthographicSize;
        currentOrthographicSize = OrthographicSize;
    }

    void Update()
    {
        float X = Input.GetAxis("Horizontal");
        float Y = Input.GetAxis("Vertical");
        Vector3 moveDir = new Vector3(X, Y, 0).normalized;
        transform.position += moveDir * Time.deltaTime * moveSpeed;
        
        currentOrthographicSize += -Input.mouseScrollDelta.y * scrollSpeed;
        currentOrthographicSize = Mathf.Clamp(currentOrthographicSize, 10, 30);
        OrthographicSize = Mathf.Lerp(OrthographicSize, currentOrthographicSize, Time.deltaTime * 5);
        CMVcam.m_Lens.OrthographicSize = OrthographicSize;
    }
}
