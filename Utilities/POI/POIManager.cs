using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public static class POIManager
{
    public static Dictionary<Transform,GameObject> InitialIZEPoiSetting(Transform[] posInScene,Canvas canvas, GameObject instancePrefab)
    {
        Dictionary<Transform, GameObject> PoiCollection = new Dictionary<Transform, GameObject>();
        foreach (var pos in posInScene)
        {
            if(canvas.GetComponent<Transform>().Find(pos.name)==null)
            {
                GameObject poiinstance = GameObject.Instantiate(instancePrefab);
                poiinstance.GetComponentInChildren<TextMeshProUGUI>().text = pos.name;
                poiinstance.SetActive(false);
                PoiCollection.Add(pos,poiinstance);
            }
        }
        return PoiCollection;
    }
    public static void UpdatePOIs(Canvas canvas,Dictionary<Transform,GameObject> PoiCollection)
    {
        foreach (var t in PoiCollection)
        {
            GameObject poiinstance = t.Value;
            RectTransform canvasRect = canvas.GetComponent<RectTransform>();
            //世界坐标转换为屏幕坐标
            Vector3 screenPos = Camera.main.WorldToScreenPoint(t.Key.position);
            Vector2 targetPos;
            //将屏幕坐标转换为ugui坐标
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPos, null, out targetPos);

            poiinstance.GetComponent<RectTransform>().anchoredPosition = targetPos;

            poiinstance.transform.SetParent(canvasRect, false);
        }
    }
    /// <summary>
    /// 显示或关闭指定的poi标记
    /// </summary>
    /// <param name="PoiCollection"></param>
    public static void ChangePoiStatus(Dictionary<Transform, GameObject> PoiCollection,bool isshow) 
    {
        foreach(var t in PoiCollection)
        {
            t.Value.gameObject.SetActive(isshow);
        }
    }
}
