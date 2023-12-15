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
    /// ���� �ɺڱ��
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
    /// ����
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
    // �л�������ʼ��
    public void ClearFadeCanvas()
    {
        fadeCanvas =null;
    }
}
