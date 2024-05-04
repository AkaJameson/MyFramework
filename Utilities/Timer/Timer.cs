using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static TimerFactory;

public class Timer 
{
	/// <summary>
	/// 计时器名字
	/// </summary>
    public string timerName { get; set; }
	/// <summary>
	/// 协程
	/// </summary>
	public Coroutine coroutine;
    /// <summary>
    /// 是否运行
    /// </summary>
    private bool isActive;
	/// <summary>
	/// 当Time发生的CallBack事件
	/// </summary>
	public event TimerCallBackHandler OnTimerCallBack;
	/// <summary>
	/// 计时类型
	/// </summary>
	private TimerType timerType;

	private float timerCount;
	/// <summary>
	/// 规定时间
	/// </summary>
	public float TimerCount
	{
		get { return timerCount; }
		set { timerCount = value; }
	}

	public bool IsActive
	{
		get { return isActive; }
		set { isActive = value; }
	}

	public Timer(string name ,bool IsActive,TimerType timerType,float time)
	{
		this.timerName = name;

		this.timerType = timerType;

		this.IsActive = IsActive;
		
		this.timerCount = time;

		//当前创建事件，等待观察者指定事件来进行使用。
		OnTimerCallBack = null;
	}

	/// <summary>
	/// 计时单元
	/// </summary>
	/// <returns></returns>
	public IEnumerator StartTimer(TimeEventArgs args)
	{
        if (timerType == TimerType.RUNONCE)
        {
			yield return new WaitForSeconds(timerCount);
			///当时间结束,触发事件
			OnTimerCallBack(this,args);
        }
		else if(timerType == TimerType.CONTINUE)
		{
			while (true)
			{
				yield return new WaitForSeconds(timerCount);
                OnTimerCallBack(this, args);
            }
        }
		else
		{
			yield return null;
		}

    }

}
