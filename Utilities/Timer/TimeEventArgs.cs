using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Time事件发生单元,预留这个参数单元，等待拓展更加复杂的功能使用
/// </summary>
public class TimeEventArgs : EventArgs
{
    /// <summary>
    /// 当前小时
    /// </summary>
    public int nowHour { get { return DateTime.Now.Hour; } }
    /// <summary>
    /// 当前分钟
    /// </summary>
    public int nowMinute { get { return DateTime.Now.Minute; } }
    /// <summary>
    /// 当前time
    /// </summary>
    public int nowSecond { get { return DateTime.Now.Second; } }

    public TimeEventArgs()
    {
            
    }
}
