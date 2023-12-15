using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeFunc : Singleton<FadeFunc>
{
    private GameObject fadeCanvas;
    private GameObject Canvas;

    public FadeFunc()
    {
        Canvas = GameObject.Find("Canvas");
    }
    /// <summary>
    /// 淡入 由黑变白
    /// </summary>
    public void FadeIn(float fadeTime)
    {
        if(fadeCanvas == null)
        {
            fadeCanvas = GameObject.Instantiate((GameObject)Resources.Load("FadePanel"));

            fadeCanvas.transform.SetParent(Canvas.transform, false);

        }
        else
        {
            fadeCanvas.SetActive(true);

        }
        fadeCanvas.GetComponent<FadeInOut>().fademode = FadeMode.IN;

        fadeCanvas.GetComponent<CanvasGroup>().alpha = 1.0f;

        fadeCanvas.GetComponent<FadeInOut>().TempFadeValue = fadeTime;

        fadeCanvas.GetComponent<FadeInOut>().fadeTime = fadeTime;
    }
    /// <summary>
    /// 淡出
    /// </summary>
    public void FadeOut(float fadeTime)
    {
        if (fadeCanvas == null)
        {
            fadeCanvas = GameObject.Instantiate((GameObject)Resources.Load("FadePanel"));

            fadeCanvas.transform.SetParent(Canvas.transform, false);
        }
        else
        {
            fadeCanvas.SetActive(true);
        }

        fadeCanvas.GetComponent<FadeInOut>().fademode = FadeMode.OUT;

        fadeCanvas.GetComponent<CanvasGroup>().alpha = 0f;

        fadeCanvas.GetComponent<FadeInOut>().TempFadeValue = 0f;

        fadeCanvas.GetComponent<FadeInOut>().fadeTime = fadeTime;
    }
    // 切换场景初始化
    public void ClearFadeCanvas()
    {
        fadeCanvas =null;
    }
}
