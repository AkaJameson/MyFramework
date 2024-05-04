using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Time�¼�������Ԫ,Ԥ�����������Ԫ���ȴ���չ���Ӹ��ӵĹ���ʹ��
/// </summary>
public class TimeEventArgs : EventArgs
{
    /// <summary>
    /// ��ǰСʱ
    /// </summary>
    public int nowHour { get { return DateTime.Now.Hour; } }
    /// <summary>
    /// ��ǰ����
    /// </summary>
    public int nowMinute { get { return DateTime.Now.Minute; } }
    /// <summary>
    /// ��ǰtime
    /// </summary>
    public int nowSecond { get { return DateTime.Now.Second; } }

    public TimeEventArgs()
    {
            
    }
}
