using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseContentScrollDown : MonoBehaviour
{
    private RectTransform rt;
    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ScrollDown()
    {
        Vector2 anchorPosition = rt.anchoredPosition;
        anchorPosition.y = rt.sizeDelta.y;
        anchorPosition.y = Mathf.Max(0, rt.sizeDelta.y);
        rt.anchoredPosition = anchorPosition;
    }
}
