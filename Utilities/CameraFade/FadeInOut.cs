using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ���Ƶ��뵭��Ч�� 
/// </summary>
public class FadeInOut : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    public FadeMode fademode;

    public float fadeTime;
    /// <summary>
    /// ��¼fade��ʱ��
    /// </summary>
    public float TempFadeValue;

    private void Start()
    {
        canvasGroup = this.transform.GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if(fademode == FadeMode.IN)
        {
            canvasGroup.alpha = (TempFadeValue -= Time.deltaTime) / fadeTime;
            if (canvasGroup.alpha < 0.05)
            {
                TempFadeValue = 0 ;
                this.transform.SetAsLastSibling();
                this.gameObject.SetActive(false);
            }
        }
        if(fademode == FadeMode.OUT)
        {
            canvasGroup.alpha = (TempFadeValue += Time.deltaTime) / fadeTime;
            if(canvasGroup.alpha > 0.95f)
            {
                TempFadeValue = 0;
                this.transform.SetAsLastSibling();
                this.gameObject.SetActive(false);
            }
        }
    }

    

}
public enum FadeMode
{
    IN,
    OUT
}
