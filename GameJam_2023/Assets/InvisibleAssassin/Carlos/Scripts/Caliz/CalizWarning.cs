using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalizWarning : MonoBehaviour
{
    [SerializeField] private Image warningIMG;
    [SerializeField] private Transform caliz;

    private void Awake()
    {
        caliz = GameObject.FindWithTag("Caliz").transform.GetChild(1);
    }

    private void Update()
    {
        float minX = warningIMG.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;

        float minY = warningIMG.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;

        Vector2 pos = Camera.main.WorldToScreenPoint(caliz.position);

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        warningIMG.transform.position = pos;
    }
}
