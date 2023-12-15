using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using XJFramework.Event;
/// <summary>
/// ��dictory������չ
/// </summary>
public static class DictionaryExtension
{
    /// <summary>
    /// ���Ը���key�õ�value���õ���ֱ�ӷ���value��û�еõ�ֱ�ӷ���null
    /// this Dictionary<Tkey,Tvalue> dict ����ֵ��ʾ����Ҫ��ȡֵ���ֵ�
    /// </summary>
    public static Tvalue TryGet<Tkey,Tvalue>(this Dictionary<Tkey,Tvalue> dict,Tkey key)
    {
        Tvalue value;
        dict.TryGetValue(key, out value);
        return value;
    }

    /// <summary>
    /// �����ֵ�Ļص��������������Ӧ����EventCenter��ע��
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
    /// �����ֵ�Ļص��������������Ӧ����EventCenter��ע��
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
