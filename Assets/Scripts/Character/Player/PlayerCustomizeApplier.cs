using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerCustomizeApplier : MonoBehaviour
{
    public enum Parts
    {
        Bag,
        Bottom,
        Eyewear,
        Face,
        Glove,
        Hair,
        Headgear,
        Shoes,
        Top
    }

    public void ApplyCustomize(Dictionary<string, int> customData)
    {
        foreach (var data  in customData)
        {
            Parts part;

            switch (data.Key)
            {
                case "Bag": part = Parts.Bag; break;
                case "Bottom": part = Parts.Bottom; break;
                case "Eyewear": part = Parts.Eyewear; break;
                case "Face": part = Parts.Face; break;
                case "Glove": part = Parts.Glove; break;
                case "Hair": part = Parts.Hair; break;
                case "Headgear": part = Parts.Headgear; break;
                case "Shoes": part = Parts.Shoes; break;
                case "Top": part = Parts.Top; break;

                default: part = Parts.Bag; break;
            }

            ApplyCustomize(part, data.Value);
        }
    }

    private void ApplyCustomize(Parts part, int index)
    {
        var curPart = transform.GetChild((int)part);

        // 이미 그 커스터마이즈라면 함수를 끝냄
        if (curPart.GetChild(index).gameObject.activeSelf == true) return;

        for(int i = 0; i < curPart.childCount; i++)
        {
            var child = curPart.GetChild(i).gameObject;
            if (child.activeSelf == true) child.SetActive(false);
        }

        curPart.GetChild(index).gameObject.SetActive(true);
    }
}
