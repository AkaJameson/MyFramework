using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using XJFramework.Event;
/// <summary>
/// 对dictory进行拓展
/// </summary>
public static class DictionaryExtension
{
    /// <summary>
    /// 尝试根据key得到value，得到了直接返回value，没有得到直接返回null
    /// this Dictionary<Tkey,Tvalue> dict 这个字典表示我们要获取值的字典
    /// </summary>
    public static Tvalue TryGet<Tkey,Tvalue>(this Dictionary<Tkey,Tvalue> dict,Tkey key)
    {
        Tvalue value;
        dict.TryGetValue(key, out value);
        return value;
    }

    /// <summary>
    /// 更改字典的回调函数，这个函数应该在EventCenter中注册
    /// </summary>
    /// <typeparam name="Tkey"></typeparam>
    /// <typeparam name="Tvalue"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <param name="dict"></param>
    /// <param name="key"></param>
    /// <param name="changeValue"></param>
    /// <param name="EventName"></param>
    /// <param name="args"></param>
    public static void ChangeDicValueCallBack<Tkey,Tvalue,T1>(this Dictionary<Tkey, Tvalue> dict,Tkey key,Tvalue changeValue,string EventName,T1 args)
    {
        dict[key] = changeValue;
        EventCenter.Instance.EventTrigger<T1>(EventName, args);
    }
    /// <summary>
    /// 更改字典的回调函数，这个函数应该在EventCenter中注册
    /// </summary>
    /// <typeparam name="Tkey"></typeparam>
    /// <typeparam name="Tvalue"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="dict"></param>
    /// <param name="key"></param>
    /// <param name="changeValue"></param>
    /// <param name="EventName"></param>
    /// <param name="arg1"></param>
    /// <param name="arg2"></param>
    public static void ChangeDicValueCallBack<Tkey, Tvalue, T1,T2>(this Dictionary<Tkey, Tvalue> dict, Tkey key, Tvalue changeValue, string EventName, T1 arg1,T2 arg2)
    {
        dict[key] = changeValue;
        EventCenter.Instance.EventTrigger<T1,T2>(EventName, arg1,arg2);
    }
}
