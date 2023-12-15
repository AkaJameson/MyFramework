using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace XJFramework.UIFramework
{
    [RequireComponent(typeof(CanvasGroup))]
    public class BasePanel : MonoBehaviour
    {
       [HideInInspector] public CanvasGroup canvasGroup;
        [HideInInspector] public string panelName;
        public virtual void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            panelName = this.transform.name;
        }
        public virtual void Start()
        {

        }
        /// <summary>
        /// 界面被显示出来
        /// </summary>

        public virtual void OnEnter()
        {
            ExtensionPanelAction.ShowPanel(canvasGroup);        
        }
        /// <summary>
        /// 界面暂停
        /// </summary>
        public virtual void OnPause()
        {
            
        }
        /// <summary>
        /// 界面恢复
        /// </summary>

        public virtual void OnResume()
        {

        }

        /// <summary>
        /// 界面退出
        /// </summary>
        public virtual void OnExit()
        {
            ExtensionPanelAction.ClosePanel(canvasGroup);
        }
    }
    
}
