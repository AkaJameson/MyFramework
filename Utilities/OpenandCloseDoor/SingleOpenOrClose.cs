using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public enum DoorStatus
{
    Left,
    Right
}
public class SingleOpenOrClose : MonoBehaviour
{
    public DoorStatus doorStatus;
    public DG.Tweening.Sequence doorSequence;
    public void OnTriggerEnter(Collider other)
    {
        if ( doorSequence!= null && doorSequence.IsPlaying()) return; // ����������ڲ��ţ��򲻽����µĶ���
        if(doorStatus==DoorStatus.Left)
        {
            doorSequence = DOTween.Sequence();
            doorSequence.Append(this.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 0,90f), 0.5f));
        }
        else
        {
            doorSequence = DOTween.Sequence();
            doorSequence.Append(this.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, -90f), 0.5f));
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (doorSequence != null && doorSequence.IsPlaying()) return; // ����������ڲ��ţ��򲻽����µĶ���

        doorSequence = DOTween.Sequence();
        doorSequence.Append(this.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, 0), 0.5f));
    }
}
