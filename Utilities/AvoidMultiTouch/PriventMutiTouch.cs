using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
namespace XJFramework.Utilites.AvoidMultiTouch
{
    /// <summary>
    /// 基于Unitask的线程管理方法,实现防止button被多次点击逻辑
    /// </summary>
    public class PriventMutiTouch : Singleton<PriventMutiTouch>
    {
        public IEnumerator DelayForSettingTime(Transform transform, float Time)
        {
            transform.GetComponent<Button>().enabled = false;
            yield return new WaitForSeconds(Time);
            transform.GetComponent<Button>().enabled = true;
        }
    }

}
