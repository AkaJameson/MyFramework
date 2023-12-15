using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace XJFramework.UIFramework
{
    /// <summary>
    /// 拓展面板方法
    /// </summary>
    public static class ExtensionPanelAction
    {
        /// <summary>
        /// 停用panel的mono生命周期
        /// </summary>
        /// <param name="canvasGroup"></param>
        public static void ClosePanel(CanvasGroup canvasGroup)
        {
            canvasGroup.gameObject.SetActive(false);
        }
        /// <summary>
        /// 开启panel的mono生命周期
        /// </summary>
        public static void ShowPanel(CanvasGroup canvasGroup)
        {
            canvasGroup.gameObject.SetActive(true);
        }
        /// <summary>
        /// 关闭面板显示，不停用面板在程序中mono的周期
        /// </summary>
        /// <param name="canvasGroup"></param>
        public static void ClosePanelInSight(CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
        }
        /// <summary>
        /// 打开面板的显示，不停用面板在程序中mono的周期
        /// </summary>
        /// <param name="canvasGroup"></param>
        public static void OpenPanelInSight(CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
        }
        /// <summary>
        /// 关闭面板的触摸功能
        /// </summary>
        /// <param name="canvasGroup"></param>
        public static void ClosePanelTouch(CanvasGroup canvasGroup)
        {
            canvasGroup.blocksRaycasts = false;
        }
        /// <summary>
        /// 打开面板的触摸功能
        /// </summary>
        /// <param name="canvasGroup"></param>
        public static void OpenPanelTouch(CanvasGroup canvasGroup)
        {
            canvasGroup.blocksRaycasts = true;
        }
    }
}
