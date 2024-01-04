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
        /// ���ؽ���������Դ
        /// </summary>
        private static Dictionary<CameraPrefabType, BaseCam> camDic;

        /// <summary>
        /// ���������Ϣ
        /// </summary>
        private static Dictionary<CameraPrefabType, string> camPrefabTypesDic;

        private static CamManager instance;

        /// <summary>
        /// �������ֻ�������һ������Ŷ�
        /// </summary>
        private static Queue<BaseCam> camQueue;

        /// <summary>
        /// �����һ�μ�������ȡ����json�ļ���Ϣ
        /// </summary>
        private static TextAsset textAsset;

        private CamConfigInfo camconfiginfo;
        /// <summary>
        /// ���ab���а�����camera����������λ��,ÿ������������Դ�İ�λ�ñ��뱩©����
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
                throw new Exception("�����쳣");
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
        /// ������������
        /// </summary>
        private void SetCamPriot(CameraPrefabType camType, int depth)
        {
            GetCam(camType).transform.GetComponent<UnityEngine.Camera>().depth = depth;
        }

        /// <summary>
        /// �������
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
        /// ���������Ԥ����Դ
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
        /// ��������
        /// </summary>
        public void DebugCamDic()
        {
            foreach (var cam in camPrefabTypesDic.Values)
            {
                Debug.Log(cam);
            }
        }
        /// <summary>
        /// ������������������Դ
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
