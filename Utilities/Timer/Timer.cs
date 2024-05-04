using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static TimerFactory;

public class Timer 
{
	/// <summary>
	/// ��ʱ������
	/// </summary>
    public string timerName { get; set; }
	/// <summary>
	/// Э��
	/// </summary>
	public Coroutine coroutine;
    /// <summary>
    /// �Ƿ�����
    /// </summary>
    private bool isActive;
	/// <summary>
	/// ��Time������CallBack�¼�
	/// </summary>
	public event TimerCallBackHandler OnTimerCallBack;
	/// <summary>
	/// ��ʱ����
	/// </summary>
	private TimerType timerType;

	private float timerCount;
	/// <summary>
	/// �涨ʱ��
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

		//��ǰ�����¼����ȴ��۲���ָ���¼�������ʹ�á�
		OnTimerCallBack = null;
	}

	/// <summary>
	/// ��ʱ��Ԫ
	/// </summary>
	/// <returns></returns>
	public IEnumerator StartTimer(TimeEventArgs args)
	{
        if (timerType == TimerType.RUNONCE)
        {
			yield return new WaitForSeconds(timerCount);
			///��ʱ�����,�����¼�
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
