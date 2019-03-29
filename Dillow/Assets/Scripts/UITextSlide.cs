using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITextSlide : MonoBehaviour
{
    RectTransform rectTransform;
    public bool showText;
    public float hideX;  // Position when off screen
    public float showX;  // Position when on screen
    public float showTime;
    public float slideSpeed;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        print(rectTransform.position.x);
        if(showText)
        {
            rectTransform.position = new Vector3(Mathf.Clamp(rectTransform.position.x + slideSpeed, hideX, showX), rectTransform.position.y, rectTransform.position.z);
        }
        else
        {
            rectTransform.position = new Vector3(Mathf.Clamp(rectTransform.position.x - slideSpeed, hideX, showX), rectTransform.position.y, rectTransform.position.z);
        }
        
    }
}
