using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace XJFramework.Camera
{
    public class BaseCam : MonoBehaviour
    {

        public CameraPrefabType PrefabType;
        /// <summary>
        /// ��ʼ��������ʹ����camδʵ����ʱ����
        /// </summary>
        public virtual void OnInitial()
        {

        }
        /// <summary>
        /// �������ǰ��Ԥ����
        /// </summary>
        public virtual void OnPreviousEnter()
        {

        }

        /// <summary>
        /// ����
        /// </summary>
        public virtual void OnEnter()
        {
            this.gameObject.SetActive(true);
        }

        /// <summary>
        /// �˳�
        /// </summary>
        public virtual void OnExit()
        {
            this.gameObject.SetActive(false);
        }
    }
}
