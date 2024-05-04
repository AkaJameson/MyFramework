using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerFactory :MonoBehaviour
{
    /// <summary>
    /// Timer����
    /// </summary>
    private static Dictionary<string, Timer> timersDic;
    /// <summary>
    /// CallBack�¼�
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="args"></param>
    public delegate void TimerCallBackHandler(object obj, TimeEventArgs args);
    /// <summary>
    /// ����
    /// </summary>
    private static TimerFactory instance;
    public static TimerFactory Instance {  get { return instance; } }
    private void Awake()
    {
        timersDic = new Dictionary<string,Timer>();
        //����һ��������ʵĵ���
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
    /// �õ���ʱ��
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Timer GetTimer(string name)
    {
        return timersDic[name];
    }
    /// <summary>
    /// ����һ��û�м����Timerʵ��,
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
    /// ע��һ���Խ���Timer����
    /// </summary>
    /// <param name="timer"></param>
    /// <returns></returns>
    public bool RegisterTimer(Timer timer)
    {
        timersDic.Add(timer.timerName, timer);
        return true;
    }
    /// <summary>
    /// ɾ����ʱ��
    /// </summary>
    /// <param name="name"></param>
    public void RemoveTimer(string name)
    {
        timersDic.Remove(name);
    }

    /// <summary>
    /// �����ض��ļ�ʱ��
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
            Debug.Log("��ʱ��IsActive δ����,�㻹û��ע���¼�");
            return false;
        }
    }

    /// <summary>
    /// �ر��ض��ļ�ʱ��
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
            Debug.LogError("��ʱ��IsActiveΪ����㻹û��ע���¼�������ʱ����û������");
            return false;
        }
    }
}
