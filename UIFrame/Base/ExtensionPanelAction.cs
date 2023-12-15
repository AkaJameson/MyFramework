using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace XJFramework.UIFramework
{
    /// <summary>
    /// ��չ��巽��
    /// </summary>
    public static class ExtensionPanelAction
    {
        /// <summary>
        /// ͣ��panel��mono��������
        /// </summary>
        /// <param name="canvasGroup"></param>
        public static void ClosePanel(CanvasGroup canvasGroup)
        {
            canvasGroup.gameObject.SetActive(false);
        }
        /// <summary>
        /// ����panel��mono��������
        /// </summary>
        public static void ShowPanel(CanvasGroup canvasGroup)
        {
            canvasGroup.gameObject.SetActive(true);
        }
        /// <summary>
        /// �ر������ʾ����ͣ������ڳ�����mono������
        /// </summary>
        /// <param name="canvasGroup"></param>
        public static void ClosePanelInSight(CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
        }
        /// <summary>
        /// ��������ʾ����ͣ������ڳ�����mono������
        /// </summary>
        /// <param name="canvasGroup"></param>
        public static void OpenPanelInSight(CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
        }
        /// <summary>
        /// �ر����Ĵ�������
        /// </summary>
        /// <param name="canvasGroup"></param>
        public static void ClosePanelTouch(CanvasGroup canvasGroup)
        {
            canvasGroup.blocksRaycasts = false;
        }
        /// <summary>
        /// �����Ĵ�������
        /// </summary>
        /// <param name="canvasGroup"></param>
        public static void OpenPanelTouch(CanvasGroup canvasGroup)
        {
            canvasGroup.blocksRaycasts = true;
        }
    }
}
