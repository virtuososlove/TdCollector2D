using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TooltipUI : MonoBehaviour
{
    private TextMeshProUGUI text;
    private RectTransform backgroundRectTransform;
    private RectTransform rectTransform;
    [SerializeField] RectTransform canvasRectTransform;
    public static TooltipUI Instance { get; private set; }
    private ToolTipTimer toolTipTimer;
    private void Awake()
    {
        Instance = this;
        text = transform.Find("text").GetComponent<TextMeshProUGUI>();
        backgroundRectTransform = transform.Find("background").GetComponent<RectTransform>();
        rectTransform = GetComponent<RectTransform>();
        Hide();
    }
    private void Update()
    {
        if(toolTipTimer != null)
        {
            toolTipTimer.timer -= Time.deltaTime;
            if(toolTipTimer.timer <= 0)
            {
                Hide();
            }
        }
        rectTransform.anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.y;
    }
    public void SetText(string textString)
    {
        text.SetText(textString);
        text.ForceMeshUpdate();
        Vector2 textsize = text.GetRenderedValues(false) + new Vector2(8, 8);
        backgroundRectTransform.sizeDelta = textsize;
    }
    public void Show(string textString,ToolTipTimer toolTipTimer = null)
    {
       
        this.toolTipTimer = toolTipTimer;
        gameObject.SetActive(true);
        SetText(textString);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public class ToolTipTimer{
        public float timer;
    }

 }
