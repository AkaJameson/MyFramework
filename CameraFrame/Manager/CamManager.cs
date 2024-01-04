using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Newtonsoft.Json;
using System;

namespace XJFramework.Camera
{
    public class CamManager
    {
        /// <summary>
        /// 加载进入的相机资源
        /// </summary>
        private static Dictionary<CameraPrefabType, BaseCam> camDic;

        /// <summary>
        /// 保存加载信息
        /// </summary>
        private static Dictionary<CameraPrefabType, string> camPrefabTypesDic;

        private static CamManager instance;

        /// <summary>
        /// 相机队列只允许存在一个相机排队
        /// </summary>
        private static Queue<BaseCam> camQueue;

        /// <summary>
        /// 保存第一次加载所读取到的json文件信息
        /// </summary>
        private static TextAsset textAsset;

        private CamConfigInfo camconfiginfo;
        /// <summary>
        /// 这个ab包中包含着camera真正的所在位置,每个真正加载资源的包位置必须暴漏出来
        /// </summary>
        public AssetBundle camAssetLocation;

        private Transform camParent;

        private Transform CamParent
        {
            get
            {
                if (camParent == null)
                {
                    camParent = GameObject.Find("Cam").transform;
                }
                return camParent;
            }
        }
        public static CamManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CamManager();
                    return instance;
                }
                return instance;
            }
        }
        public CamManager()
        {
            camQueue = new Queue<BaseCam>();

            camDic = new Dictionary<CameraPrefabType, BaseCam>();

            camPrefabTypesDic = new Dictionary<CameraPrefabType, string>();

            LoadAssetBundle();
        }

        public void EnQueue(CameraPrefabType camType)
        {
            if (camQueue.Count > 1)
            {
                throw new Exception("队列异常");
            }

            if (camQueue.Count == 0)
            {
                BaseCam baseCam = GetCam(camType);
                if (baseCam != null)
                {
                    baseCam.PrefabType = camType;
                    baseCam.OnPreviousEnter();
                    baseCam.OnEnter();
                    camQueue.Enqueue(baseCam);
                }
                
            }
            else
            {
               
                BaseCam baseCam = GetCam(camType);
                baseCam.PrefabType = camType;
                baseCam.OnPreviousEnter();
                BaseCam preCam = camQueue.Dequeue();
                preCam.OnExit();
                camQueue.Enqueue(baseCam);
                baseCam.OnEnter();
               
            }
        }

        /// <summary>
        /// 设置相机的深度
        /// </summary>
        private void SetCamPriot(CameraPrefabType camType, int depth)
        {
            GetCam(camType).transform.GetComponent<UnityEngine.Camera>().depth = depth;
        }

        /// <summary>
        /// 加载相机
        /// </summary>
        /// <param name="name"></param>
        public BaseCam GetCam(CameraPrefabType camType)
        {
            if (camDic == null)
            {
                camDic = new Dictionary<CameraPrefabType, BaseCam>();
            }
            if (!camDic.ContainsKey(camType))
            {
                GameObject ABCam = (GameObject)camAssetLocation.LoadAsset(camPrefabTypesDic[camType]);

                ABCam.GetComponent<BaseCam>().OnInitial();

                GameObject baseCam = GameObject.Instantiate(ABCam);

                baseCam.transform.SetParent(CamParent, false);

                camDic.Add(camType, baseCam.GetComponent<BaseCam>());

                return baseCam.GetComponent<BaseCam>();
            }
            else
            {
                BaseCam cam = camDic.TryGet(camType);

                return cam;
            }


        }

        /// <summary>
        /// 加载摄像机预设资源
        /// </summary>
        private void LoadAssetBundle()
        {
            AssetBundle assetBundle = AssetBundle.LoadFromFile("AssetBundles/StandaloneWindows/camconfigration");

            textAsset = (TextAsset)assetBundle.LoadAsset("CameraLoadType");

            CamDesierial desierial = JsonConvert.DeserializeObject<CamDesierial>(textAsset.text);

            camconfiginfo = desierial.Configuration;

            camAssetLocation = AssetBundle.LoadFromFile(desierial.Configuration.Path);

            foreach (CamListInfo camInfo in camconfiginfo.CameraList)
            {
                camPrefabTypesDic.Add(camInfo.CameraType, camInfo.Path);
            }
        }
        /// <summary>
        /// 测试用例
        /// </summary>
        public void DebugCamDic()
        {
            foreach (var cam in camPrefabTypesDic.Values)
            {
                Debug.Log(cam);
            }
        }
        /// <summary>
        /// 加载相机组件依赖的资源
        /// </summary>
        public T LoadDependancyAssetFromFile<T>(string assetName) where T : UnityEngine.Object
        {
            return camAssetLocation.LoadAsset<T>(assetName);
        }

        public void SwitchSceneClearPreviousState()
        {
            camDic.Clear();
            camQueue.Clear();
        }

        public BaseCam PeekCam()
        {
            if(camQueue.Count == 0)
            {
                return null;
            }
            return camQueue.Peek();
        }
    }
}
