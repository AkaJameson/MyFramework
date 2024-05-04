using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerFactory :MonoBehaviour
{
    /// <summary>
    /// Timer管理
    /// </summary>
    private static Dictionary<string, Timer> timersDic;
    /// <summary>
    /// CallBack事件
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="args"></param>
    public delegate void TimerCallBackHandler(object obj, TimeEventArgs args);
    /// <summary>
    /// 单例
    /// </summary>
    private static TimerFactory instance;
    public static TimerFactory Instance {  get { return instance; } }
    private void Awake()
    {
        timersDic = new Dictionary<string,Timer>();
        //创建一个方便访问的单例
        if (instance != null )
        {
            Destroy( instance );
        }
        else
        {
            instance = this;
        }
    }

    /// <summary>
    /// 得到计时器
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Timer GetTimer(string name)
    {
        return timersDic[name];
    }
    /// <summary>
    /// 创建一个没有激活的Timer实例,
    /// </summary>
    /// <param name="name"></param>
    /// <param name="isActive"></param>
    /// <param name="timerType"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public bool CreateTimer(string name,TimerType timerType,float time)
    {
        Timer timer = new Timer(name, false, timerType, time);
        timersDic.Add(name, timer);
        return true;
    }
    /// <summary>
    /// 注册一个自建的Timer对象
    /// </summary>
    /// <param name="timer"></param>
    /// <returns></returns>
    public bool RegisterTimer(Timer timer)
    {
        timersDic.Add(timer.timerName, timer);
        return true;
    }
    /// <summary>
    /// 删除计时器
    /// </summary>
    /// <param name="name"></param>
    public void RemoveTimer(string name)
    {
        timersDic.Remove(name);
    }

    /// <summary>
    /// 开启特定的计时器
    /// </summary>
    /// <param name="name"></param>
    /// <param name="args"></param>

    public bool StartTimer(string name,TimeEventArgs args)
    {
        if (timersDic[name].IsActive)
        {
            timersDic[name].coroutine = StartCoroutine(timersDic[name].StartTimer(args));
            return true;
        }
        else
        {
            Debug.Log("计时器IsActive 未激活,你还没有注册事件");
            return false;
        }
    }

    /// <summary>
    /// 关闭特定的计时器
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool StopTimer(string name)
    {
        if (timersDic[name].IsActive)
        {
            StopCoroutine(timersDic[name].coroutine);
            return true ;
        }
        else
        {
            Debug.LogError("计时器IsActive为激活，你还没有注册事件，及计时器还没有启动");
            return false;
        }
    }
}
