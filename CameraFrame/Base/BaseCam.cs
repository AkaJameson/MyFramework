using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace XJFramework.Camera
{
    public class BaseCam : MonoBehaviour
    {

        public CameraPrefabType PrefabType;
        /// <summary>
        /// 初始化操作，使用在cam未实例化时运行
        /// </summary>
        public virtual void OnInitial()
        {

        }
        /// <summary>
        /// 进入队列前的预操作
        /// </summary>
        public virtual void OnPreviousEnter()
        {

        }

        /// <summary>
        /// 进入
        /// </summary>
        public virtual void OnEnter()
        {
            this.gameObject.SetActive(true);
        }

        /// <summary>
        /// 退出
        /// </summary>
        public virtual void OnExit()
        {
            this.gameObject.SetActive(false);
        }
    }
}
