using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class TipDeserial
{
    public TipsJson Configuration;
}
[Serializable]
public class TipsJson
{
    public InfoList[] TipsList { get; set; }
    public string Path { get; set; }
}
[Serializable]
public class InfoList
{
    public TipsType TipName { get; set; }
    public string TipPath { get; set; }

}
public class TipsManager :Singleton<TipsManager>
{
    private Dictionary<TipsType, string> TipsPathDic;
    private Dictionary<TipsType,BaseTips> tipsDic;
    private AssetBundle TipBundle;
    private Canvas canvas;
    public TipsManager()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        if (tipsDic == null)
        { 
            tipsDic = new Dictionary<TipsType, BaseTips>();
            TipsPathDic = new Dictionary<TipsType, string>();   

            LoadTipsInfoToEntity();
        }
    }
    /// <summary>
    /// 初始化LoadTips
    /// </summary>
    private void LoadTipsInfoToEntity()
    {
        AssetBundle Tipconfiguration = AssetBundle.LoadFromFile("AssetBundles/StandaloneWindows/tipsconfiguration");

        TextAsset textAsset = (TextAsset)Tipconfiguration.LoadAsset("TipsTypes");

        TipDeserial tipDeserial = JsonConvert.DeserializeObject<TipDeserial>(textAsset.text);

        TipBundle = AssetBundle.LoadFromFile(tipDeserial.Configuration.Path);

        foreach(var t in tipDeserial.Configuration.TipsList)
        {
            TipsPathDic.Add(t.TipName, t.TipPath);
        }
        Tipconfiguration.Unload(true);
    }
    /// <summary>
    /// 进入显示
    /// </summary>
    /// <param name="tipsType"></param>
    public void ShowingTipsInScreen(TipsType tipsType)
    {
        if(!tipsDic.ContainsKey(tipsType))
        {

            GameObject go = TipBundle.LoadAsset<GameObject>(TipsPathDic[tipsType]);

            BaseTips goInScene = GameObject.Instantiate<GameObject>(go).GetComponent<BaseTips>();

            goInScene.transform.SetParent(canvas.transform, false);

            tipsDic.Add(tipsType, goInScene);

            goInScene.OnEnter();
        }
        else
        {
            BaseTips baseTips = tipsDic.TryGet(tipsType);
            baseTips.OnEnter();
        }
    }
    /// <summary>
    /// 退出显示
    /// </summary>
    /// <param name="tipsType"></param>
    public void CloseTipsInScreen(TipsType tipsType)
    {
        tipsDic.TryGet(tipsType).OnExit();
    }
}




