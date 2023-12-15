using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace XJFramework.Utilites
{
    [RequireComponent(typeof(BoxCollider))]
    public class OpenOrCloseDoor : MonoBehaviour
    {
        public float leftOpenAngle = 90f;
        public float rightOpenAngle = -90f;
        [SerializeField]
        private Transform LeftDoor;
        [SerializeField]
        private Transform RightDoor;
        private BoxCollider boxCollider;
        private bool isRotate;
        private Sequence doorSequence;
        private void Awake()
        {
            LeftDoor = this.transform.Find("LeftDoor").transform;

            RightDoor = this.transform.Find("RightDoor").transform;

            boxCollider = this.transform.GetComponent<BoxCollider>();
            
            AutoSetBoxCollideSize();
        }



        public void OnTriggerEnter(Collider other)
        {
            if (doorSequence != null && doorSequence.IsPlaying()) return; // 如果动画正在播放，则不进行新的动画

            doorSequence = DOTween.Sequence();
            doorSequence.Append(LeftDoor.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, leftOpenAngle), 0.5f));
            doorSequence.Join(RightDoor.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, rightOpenAngle), 0.5f));
        }

        public void OnTriggerExit(Collider other)
        {
            if (doorSequence != null && doorSequence.IsPlaying()) return; // 如果动画正在播放，则不进行新的动画

            doorSequence = DOTween.Sequence();
            doorSequence.Append(LeftDoor.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, 0), 0.5f));
            doorSequence.Join(RightDoor.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, 0), 0.5f));
        }
        /// <summary>
        /// 自动初始化脚本
        /// </summary>
        private void AutoSetBoxCollideSize()
        {
            boxCollider.size = new Vector3(2.5f, 6.5f, 3.5f);
            boxCollider.center = new Vector3(0, 0, 0);
            boxCollider.isTrigger = true;
        }
    }
}
