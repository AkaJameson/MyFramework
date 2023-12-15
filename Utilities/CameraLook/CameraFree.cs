using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;


namespace CameraLook
{
    public class CameraFree : MonoBehaviour
    {
        [Header("移动速度")]
        public float moveSpeed = 1.2f;
        [Header("缩进速度")]
        public float zoomSpeed = 0.2f;
        [Header("旋转速度")]
        public float rotateSpeed = 60f;

        [Header("最小高度")]
        public float minHeight = 0f;
        [Header("最大高度")]
        public float maxHeight = 1000f;
        [Header("最小倾角")]
        [Range(0, -88)]
        public float minTilt = -60f;
        [Header("最大倾角")]
        [Range(0, 88)]
        public float maxTilt = 88f;
        [Header("地图中心点")]
        public Vector3 mapCenter = Vector3.zero;
        [Header("相机距离中心点最大距离")]
        public float maxDistance = 5000f;



        private float moveDamping = 5f;
        private float rotateDamping = 5f;


        //回调
        public UnityAction maxDistanceCallback = null;




        //控制变量
        private Vector3 viewPos_0;         //鼠标左键初始坐标
        private Vector3 viewPos_1;         //鼠标右键初始坐标
        private float zoomDis;             //双指触控时初始距离

        private Vector3 startCameraPos;           //相机初始位置
        private Vector3 targetCameraPos;          //相机目标位置

        private Vector2 startCameraEuler;         //初始角度
        private Vector2 targetCameraEuler;        //目标角度

        private Vector3 startFollowDir;           //相机跟随 - 初始距离

        //相机控制
        private Camera mainCamera;
        //相机是否可控制
        private bool isCameraCtrl = true;
        //相机操作有效性判断
        private bool isClickUI = false;

        //触屏操作
        private int touchCount = 0;




        private void Awake()
        {
            mainCamera = Camera.main;

            InitCameraFreeMode();
        }

        void LateUpdate()
        {
            //相机控制
            if (isCameraCtrl)
            {
              /*  //键鼠操作
                if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
                {
                    if (EventSystem.current != null)
                    {
                        isClickUI = EventSystem.current.IsPointerOverGameObject();
                        *//*                    Debug.Log(EventSystem.current.currentSelectedGameObject);*//*
                    }
                }*/
                if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
                {
                    isClickUI = false;
                }
                //触屏操作
                if (touchCount != Input.touchCount && EventSystem.current != null)
                {
                    if (Input.touchCount > 0)
                    {
                        isClickUI = EventSystem.current.IsPointerOverGameObject();
                    }
                    else
                    {
                        isClickUI = false;
                    }
                }
                //相机操作
                if (!isClickUI)
                {
                    CameraFreeMode();
                }
                //触摸点数量
                if (touchCount != Input.touchCount)
                {
                    touchCount = Input.touchCount;
                }

            }
        }



        #region 相机控制

        //相机控制
        public void IsCameraControl(bool isControl)
        {
            isCameraCtrl = isControl;
            if (isControl)
            {
                isClickUI = false;
                viewPos_0 = mainCamera.ScreenToViewportPoint(Input.mousePosition);
                viewPos_1 = mainCamera.ScreenToViewportPoint(Input.mousePosition);
                startCameraPos = transform.position;
                startCameraEuler = transform.eulerAngles;
                targetCameraPos = transform.position;
                targetCameraEuler = transform.eulerAngles;
            }
        }


        //初始化相机 - 自由模式
        public void InitCameraFreeMode()
        {
            viewPos_0 = mainCamera.ScreenToViewportPoint(Input.mousePosition);
            viewPos_1 = mainCamera.ScreenToViewportPoint(Input.mousePosition);
            startCameraPos = transform.position;
            startCameraEuler = transform.eulerAngles;
            targetCameraPos = transform.position;
            targetCameraEuler = transform.eulerAngles;
        }









        //自由相机模式
        void CameraFreeMode()
        {
            //平移
            if (Input.GetMouseButtonDown(0))
            {
                viewPos_0 = mainCamera.ScreenToViewportPoint(Input.mousePosition);
                startCameraPos = transform.position;
                startCameraEuler = transform.eulerAngles;
            }
            if (Input.GetMouseButton(0))
            {
                Vector3 dis = mainCamera.ScreenToViewportPoint(Input.mousePosition) - viewPos_0;
                dis = dis * GetMoveSpeed();
                dis = new Vector3(-dis.x * 3f, 0f, -dis.y);
                dis = Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up) * dis;
                targetCameraPos = startCameraPos + dis;
            }
            //旋转
            if (Input.GetMouseButtonDown(1))
            {
                viewPos_1 = mainCamera.ScreenToViewportPoint(Input.mousePosition);
                startCameraPos = transform.position;
                startCameraEuler = transform.eulerAngles;
            }
            if (Input.GetMouseButton(1))
            {
                Vector2 angle = (mainCamera.ScreenToViewportPoint(Input.mousePosition) - viewPos_1) * rotateSpeed;
                angle = new Vector3(-angle.y, angle.x);
                targetCameraEuler = startCameraEuler + angle;
            }
            //缩进
            if (Input.mouseScrollDelta != Vector2.zero)
            {
                targetCameraPos += transform.forward * GetZoomSpeed() * Input.mouseScrollDelta.normalized.y;
            }
            //键盘控制
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                float speed = GetMoveSpeed();
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    speed = GetMoveSpeed() * 3f;
                }
                targetCameraPos += (transform.forward * speed * Input.GetAxis("Vertical") * Time.deltaTime + transform.right * speed * Input.GetAxis("Horizontal") * Time.deltaTime);
            }
            //触屏控制
            if (Input.touchCount > 0)
            {
                //平移
                if (Input.touchCount != touchCount && Input.touchCount == 1)
                {
                    viewPos_0 = mainCamera.ScreenToViewportPoint(Input.GetTouch(0).position);
                    startCameraPos = transform.position;
                    startCameraEuler = transform.eulerAngles;
                }
                if (Input.touchCount == 1)
                {
                    Vector3 dis = mainCamera.ScreenToViewportPoint(Input.GetTouch(0).position) - viewPos_0;
                    dis = dis * GetMoveSpeed();
                    dis = new Vector3(-dis.x * 3f, 0f, -dis.y);
                    dis = Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up) * dis;
                    targetCameraPos = startCameraPos + dis;
                }
                //旋转、缩进
                if (Input.touchCount != touchCount && Input.touchCount == 2)
                {
                    Vector3 centerPos = (Input.GetTouch(0).position + Input.GetTouch(1).position) / 2f;
                    Vector3 dir = Input.GetTouch(0).position - Input.GetTouch(1).position;
                    viewPos_1 = mainCamera.ScreenToViewportPoint(centerPos);
                    zoomDis = mainCamera.ScreenToViewportPoint(dir).magnitude;
                    startCameraPos = transform.position;
                    startCameraEuler = transform.eulerAngles;
                }
                if (Input.touchCount == 2)
                {
                    Vector3 centerPos = (Input.GetTouch(0).position + Input.GetTouch(1).position) / 2f;
                    Vector2 angle = (mainCamera.ScreenToViewportPoint(centerPos) - viewPos_1) * rotateSpeed;
                    angle = new Vector3(-angle.y, angle.x);
                    targetCameraEuler = startCameraEuler + angle;

                    Vector3 dir = Input.GetTouch(0).position - Input.GetTouch(1).position;
                    float dis = mainCamera.ScreenToViewportPoint(dir).magnitude;
                    targetCameraPos = startCameraPos + transform.forward * GetZoomSpeed() * 10f * (dis - zoomDis);
                }
            }

            //相机限制(自由相机不限制最小距离)
            LimitCamera();
            if (Vector3.Distance(targetCameraPos, mapCenter) > maxDistance && maxDistance != 0f)
            {
                targetCameraPos = mapCenter + (targetCameraPos - mapCenter).normalized * maxDistance;
            }
            //控制相机运动
            if (Vector3.Distance(transform.position, targetCameraPos) > 0.03f)
            {
                transform.position = Vector3.Lerp(transform.position, targetCameraPos, moveDamping * Time.deltaTime);
            }
            //控制相机旋转
            if (Quaternion.Angle(transform.rotation, Quaternion.Euler(targetCameraEuler)) > 0.03f)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetCameraEuler), rotateDamping * Time.deltaTime);
            }
        }



        #endregion




        //获取相机移动速度
        public float GetMoveSpeed()
        {
            float h = targetCameraPos.y - minHeight;
            float speed = h * moveSpeed;
            if (speed < 0.2f)
            {
                speed = 0.2f;
            }
            return speed;
        }

        //获取相机缩进速度
        float GetZoomSpeed()
        {
            float speed = (targetCameraPos.y - minHeight) * zoomSpeed;
            if (speed < 0.1f)
            {
                speed = 0.1f;
            }
            return speed;
        }

        //限制相机位置，角度
        void LimitCamera()
        {
            //高度限制
            if (maxHeight - minHeight > 0f)
            {
                targetCameraPos.y = Mathf.Clamp(targetCameraPos.y, minHeight, maxHeight);
            }

            //角度限制
            if (targetCameraEuler.x > 180) { targetCameraEuler.x -= 360f; }
            if (targetCameraEuler.x < -180) { targetCameraEuler.x += 360; }
            if (targetCameraEuler.y > 180) { targetCameraEuler.y -= 360f; }
            if (targetCameraEuler.y < -180) { targetCameraEuler.y += 360; }

            targetCameraEuler.x = Mathf.Clamp(targetCameraEuler.x, minTilt, maxTilt);
        }

        //标准化角度显示
        float NormAngle(float angle)
        {
            float res = angle % 360f;
            if (angle > 180f)
            {
                res = angle -= 360f;
            }
            if (angle < -180f)
            {
                res = angle + 360f;
            }
            return res;
        }

    }
}



