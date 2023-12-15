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
        /// ���汻��ʾ����
        /// </summary>

        public virtual void OnEnter()
        {
            ExtensionPanelAction.ShowPanel(canvasGroup);        
        }
        /// <summary>
        /// ������ͣ
        /// </summary>
        public virtual void OnPause()
        {
            
        }
        /// <summary>
        /// ����ָ�
        /// </summary>

        public virtual void OnResume()
        {

        }

        /// <summary>
        /// �����˳�
        /// </summary>
        public virtual void OnExit()
        {
            ExtensionPanelAction.ClosePanel(canvasGroup);
        }
    }
    
}
