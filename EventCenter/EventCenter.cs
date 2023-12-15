using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace XJFramework.Event
{
    public class EventCenter : Singleton<EventCenter>
    {
        /// <summary>
        /// 订阅事件的列表
        /// </summary>
        private Dictionary<string, IeventInfo> _eventDic = new Dictionary<string, IeventInfo>();

        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        public void AddEventListener(string name, UnityAction action)
        {
            if (_eventDic.ContainsKey(name))
            {
                (_eventDic[name] as EventInfo).action += action;
            }
            else
            {
                _eventDic.Add(name, new EventInfo(action));
            }
        }
        public void AddEventListener<T>(string name, UnityAction<T> action)
        {
            if (_eventDic.ContainsKey(name))
            {
                (_eventDic[name] as EventInfo<T>).action += action;
            }
            else
            {
                _eventDic.Add(name, new EventInfo<T>(action));
            }
        }
        public void AddEventListener<T1, T2>(string name, UnityAction<T1, T2> action)
        {
            if (_eventDic.ContainsKey(name))
            {
                (_eventDic[name] as EventInfo<T1, T2>).action += action;
            }
            else
            {
                _eventDic.Add(name, new EventInfo<T1, T2>(action));
            }
        }
        public void AddEventListener<T1, T2, T3>(string name, UnityAction<T1, T2, T3> action)
        {
            if (_eventDic.ContainsKey(name))
            {
                (_eventDic[name] as EventInfo<T1, T2, T3>).action += action;
            }
            else
            {
                _eventDic.Add(name, new EventInfo<T1, T2, T3>(action));
            }
        }
        public void AddEventListener<T1, T2, T3, T4>(string name, UnityAction<T1, T2, T3, T4> action)
        {
            if (_eventDic.ContainsKey(name))
            {
                (_eventDic[name] as EventInfo<T1, T2, T3, T4>).action += action;
            }
            else
            {
                _eventDic.Add(name, new EventInfo<T1, T2, T3, T4>(action));
            }
        }
        public void AddEventListener<T1, T2, T3, T4, T5>(string name, Action<T1, T2, T3, T4, T5> action)
        {
            if (_eventDic.ContainsKey(name))
            {
                (_eventDic[name] as EventInfo<T1, T2, T3, T4, T5>).action += action;
            }
            else
            {
                _eventDic.Add(name, new EventInfo<T1, T2, T3, T4, T5>(action));
            }
        }
        /// <summary>
        /// 移除事件
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        public void RemoveEventListener(string name, UnityAction action)
        {
            if (_eventDic.ContainsKey(name))
            {
                (_eventDic[name] as EventInfo).action -= action;
            }
        }
        public void RemoveEventListener<T1>(string name, UnityAction<T1> action)
        {
            if (_eventDic.ContainsKey(name))
            {
                (_eventDic[name] as EventInfo<T1>).action -= action;
            }
        }
        public void RemoveEventListener<T1, T2>(string name, UnityAction<T1, T2> action)
        {
            if (_eventDic.ContainsKey(name))
            {
                (_eventDic[name] as EventInfo<T1, T2>).action -= action;
            }
        }
        public void RemoveEventListener<T1, T2, T3>(string name, UnityAction<T1, T2, T3> action)
        {
            if (_eventDic.ContainsKey(name))
            {
                (_eventDic[name] as EventInfo<T1, T2, T3>).action -= action;
            }
        }
        public void RemoveEventListener<T1, T2, T3, T4>(string name, UnityAction<T1, T2, T3, T4> action)
        {
            if (_eventDic.ContainsKey(name))
            {
                (_eventDic[name] as EventInfo<T1, T2, T3, T4>).action -= action;
            }
        }
        public void RemoveEventListener<T1, T2, T3, T4, T5>(string name, Action<T1, T2, T3, T4, T5> action)
        {
            if (_eventDic.ContainsKey(name))
            {
                (_eventDic[name] as EventInfo<T1, T2, T3, T4, T5>).action -= action;
            }
        }
        /// <summary>
        /// 触发事件
        /// </summary>
        /// <param name="name"></param>
        public void EventTrigger(string name)
        {
            if ((_eventDic[name] as EventInfo).action != null)
            {
                (_eventDic[name] as EventInfo).action.Invoke();
            }
        }
        public void EventTrigger<T1>(string name,T1 info1)
        {
            if ((_eventDic[name] as EventInfo<T1>).action != null)
            {
                (_eventDic[name] as EventInfo<T1>).action.Invoke(info1);
            }
        }
        public void EventTrigger<T1,T2>(string name, T1 info1,T2 info2)
        {
            if ((_eventDic[name] as EventInfo<T1,T2>).action != null)
            {
                (_eventDic[name] as EventInfo<T1,T2>).action.Invoke(info1,info2);
            }
        }
        public void EventTrigger<T1, T2, T3>(string name, T1 info1, T2 info2, T3 info3)
        {
            if ((_eventDic[name] as EventInfo<T1, T2, T3>).action != null)
            {
                (_eventDic[name] as EventInfo<T1, T2, T3>).action.Invoke(info1, info2, info3);
            }
        }
        public void EventTrigger<T1, T2, T3, T4>(string name, T1 info1, T2 info2, T3 info3,T4 info4)
        {
            if ((_eventDic[name] as EventInfo<T1, T2, T3, T4>).action != null)
            {
                (_eventDic[name] as EventInfo<T1, T2, T3, T4>).action.Invoke(info1, info2, info3, info4);
            }
        }
        public void EventTrigger<T1, T2, T3, T4, T5>(string name, T1 info1, T2 info2, T3 info3, T4 info4, T5 info5)
        {
            if ((_eventDic[name] as EventInfo<T1, T2, T3, T4, T5>).action != null)
            {
                (_eventDic[name] as EventInfo<T1, T2, T3, T4, T5>).action.Invoke(info1, info2, info3, info4, info5);
            }
        }
        /// <summary>
        /// 清空注册
        /// </summary>
        public void Clear()
        {
            _eventDic.Clear();
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public interface IeventInfo
    {

    }
    /// <summary>
    /// 无参构造
    /// </summary>
    public class EventInfo : IeventInfo
    {
        public UnityAction action;

        public EventInfo(UnityAction action)
        {
            this.action += action;
        }
    }
    /// <summary>
    /// 单参构造
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EventInfo<T> : IeventInfo
    {
        public UnityAction<T> action;

        public EventInfo(UnityAction<T> action)
        {
            this.action += action;
        }
    }
    /// <summary>
    /// 多参构造
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EventInfo<T1, T2> : IeventInfo
    {
        public UnityAction<T1, T2> action;

        public EventInfo(UnityAction<T1, T2> action)
        {
            this.action += action;
        }
    }
    public class EventInfo<T1, T2, T3> : IeventInfo
    {
        public UnityAction<T1, T2, T3> action;

        public EventInfo(UnityAction<T1, T2, T3> action)
        {
            this.action += action;
        }
    }
    public class EventInfo<T1, T2, T3, T4> : IeventInfo
    {
        public UnityAction<T1, T2, T3, T4> action;

        public EventInfo(UnityAction<T1, T2, T3, T4> action)
        {
            this.action += action;
        }

    }
    public class EventInfo<T1, T2, T3, T4, T5> : IeventInfo
    {
        public Action<T1, T2, T3, T4, T5> action;

        public EventInfo(Action<T1, T2, T3, T4, T5> action)
        {
            this.action = action;
        }
    }
}
