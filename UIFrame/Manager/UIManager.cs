using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Newtonsoft.Json;
using System;
namespace XJFramework.UIFramework
{
    /// <summary>
    /// �������Ȼ�����ƣ����л�������û����ӱ����ϸ�������Ϣ�ķ���������Ҫ���·����ϸ�������ʱ����Ҫ����дһ����أ������ƣ�
    /// ���ҵ�ǰ��clearmanager�൱�Ĵַţ�����˲���Ҫ��cpu���㿪��.
    /// ÿ��basepanelû���Լ�panel������TODO��
    /// </summary>
    public class UIManager
    {
        /// <summary>
        /// UIManager����
        /// </summary>
        private static UIManager instance;
        /// <summary>
        /// ����Panel������
        /// </summary>
        private Dictionary<UIPanelType, string> panelPathDict;
        /// <summary>
        ///��������ʵ����������Ϸ�������ϵ�BasePanel
        /// </summary>
        private Dictionary<UIPanelType, BasePanel> panelDict;
        /// <summary>
        /// ����ui�Ĳ㼶�ṹ
        /// </summary>
        private Stack<BasePanel> panelStack;
        /// <summary>
        /// �����ϸ�����panel��Ϣ TODO
        /// </summary>
        private Stack<BasePanel> previousPanelStack;

        private Queue<BasePanel> noneInStackPanelQuene;

        private Transform canvasTransform;
        private Transform CanvasTransform
        {
            get
            {
                if (canvasTransform == null)
                    canvasTransform = GameObject.Find("Canvas").transform;
                return canvasTransform;
            }
        }
        /// <summary>
        /// ���������ɵ�ʵ�����ļ�
        /// </summary>
        private ConfigInfo panelListJson;
        /// <summary>
        /// ��������·����Ϣ
        /// </summary>

        //����uipanel��ab��
        private AssetBundle PanelFileAB;

        public static UIManager Instance
        {
            get { if (instance == null) instance = new UIManager(); return instance; }
        }
        public UIManager()
        {
            ParseUIPanelTypeJsonToUiPanelType();

            AddPanelInfoIntoDic();

        }

        public BasePanel GetPanel(UIPanelType panelType)
        {
            if (panelDict == null)
            {
                panelDict = new Dictionary<UIPanelType, BasePanel>();

            }
            if (PanelFileAB == null)
            {
                PanelFileAB = AssetBundle.LoadFromFile(panelListJson.Path);
            }
            BasePanel panel = panelDict.TryGet(panelType);
            if (panel == null)
            {
                //����Ҳ�������ô���ҵ��������prefab·����Ȼ�����prefabȥʵ�������
                string path = panelPathDict.TryGet(panelType);

                GameObject instPanel = GameObject.Instantiate(PanelFileAB.LoadAsset<GameObject>(path));

                instPanel.transform.SetParent(CanvasTransform, false);

                panelDict.Add(panelType, instPanel.GetComponent<BasePanel>());

                return instPanel.transform.GetComponent<BasePanel>();
            }
            else
            {
                return panel;
            }
        }

        public class Deserial
        {
            public ConfigInfo Configuration;
        }
        /// <summary>
        /// ��ȡ�����ļ�
        /// </summary>

        private void ParseUIPanelTypeJsonToUiPanelType()
        {
            AssetBundle configuration = AssetBundle.LoadFromFile("AssetBundles/StandaloneWindows/uiconfigration");

            UnityEngine.TextAsset ta = (UnityEngine.TextAsset)configuration.LoadAsset("UIPanelType");

            Deserial panelListJson = JsonConvert.DeserializeObject<Deserial>(ta.text);

            this.panelListJson = panelListJson.Configuration;

            configuration.Unload(true);
        }
        /// <summary>
        /// ����ȡ����panel��Ϣ���ص�UIManager���ֵ���
        /// </summary>
        private void AddPanelInfoIntoDic()
        {
            panelPathDict = new Dictionary<UIPanelType, string>();

            foreach (var panel in panelListJson.PanelList)
            {

                panelPathDict.Add(panel.PanelType, panel.Path);

            }
        }

        /// <summary>
        /// ��ĳ��ҳ����ջ����ĳ��ҳ����ʾ��ҳ���ϣ����Ҵ���ÿ��ҳ���OnEnter����
        /// </summary>
        public void PushPanel(UIPanelType panelType)
        {
            if (panelStack == null)
                panelStack = new Stack<BasePanel>();
            //�ж�һ��ջ�����Ƿ���ҳ��,����У�����ջ������pause()����
            if (panelStack.Count > 0)
            {
                BasePanel topPanel = panelStack.Peek();
                topPanel.OnPause();
            }
            BasePanel panel = GetPanel(panelType);
            panel.OnEnter();
            panelStack.Push(panel);
        }
        /// <summary>
        /// ��ĳ��ҳ���ջ���ر�ҳ�����ʾ
        /// </summary>
        public void PopPanel()
        {
            if (panelStack == null)
                panelStack = new Stack<BasePanel>();
            if (panelStack.Count <= 0) { return; }
            //�ر�ջ��ҳ�����ʾ
            BasePanel topPanel = panelStack.Pop();
            topPanel.OnExit();

            if (panelStack.Count <= 0) { return; }
            BasePanel topPanel2 = panelStack.Peek();
            topPanel2.OnResume();

        }
        /// <summary>
        /// ������ ��ȡpanel�����������
        /// </summary>
        /// <typeparam name="T">UIPanelType</typeparam>
        /// <param name="type"></param>
        /// <param name="childName"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>

        /// <summary>
        /// �鿴ջ�����
        /// </summary>
        /// <returns></returns>
        public BasePanel PeekPanel()
        {
            return panelStack.Peek();
        }
        /// <summary>
        /// ���й���ui�����Ӳ���
        /// </summary>
        /// <param name="type"></param>
        public void EnQuene(UIPanelType type)
        {
            if (noneInStackPanelQuene == null)
                noneInStackPanelQuene = new Queue<BasePanel>();
            BasePanel panel = GetPanel(type);
            panel.OnEnter();
            noneInStackPanelQuene.Enqueue(panel);
        }
        /// <summary>
        /// ���й���ui�����Ӳ���
        /// </summary>
        /// <param name="type"></param>
        public void DeQuene(UIPanelType type)
        {
            if (noneInStackPanelQuene == null)
                noneInStackPanelQuene = new Queue<BasePanel>();
            if (noneInStackPanelQuene.Count > 0)
            {
                BasePanel panel = noneInStackPanelQuene.Dequeue();
                panel.OnExit();
            }
        }
        /// <summary>
        /// ��ѯ��������
        /// </summary>
        /// <returns></returns>
        public int CountQuene()
        {
            return noneInStackPanelQuene.Count;
        }

        /// <summary>
        /// ����UI������
        /// </summary>
        public void ClearUIManager()
        {
            // �������ֵ��������Դ��ȷ������ UI ��ص���Դ�����ͷŻ����
            if (instance.panelDict != null)
            {
                instance.panelDict.Clear();
                instance.panelDict = null;
            }

            if (instance.panelPathDict != null)
            {
                instance.panelPathDict.Clear();
                instance.panelPathDict = null;
            }

            if (instance.panelStack != null)
            {
                instance.panelStack.Clear();
                instance.panelStack = null;
            }

            if (instance.previousPanelStack != null)
            {
                instance.previousPanelStack.Clear();
                instance.previousPanelStack = null;
            }

            if (instance.noneInStackPanelQuene != null)
            {
                instance.noneInStackPanelQuene.Clear();
                instance.noneInStackPanelQuene = null;
            }

            // �ͷ� AssetBundle
            if (instance.PanelFileAB != null)
            {
                instance.PanelFileAB.Unload(true);
                instance.PanelFileAB = null;
            }

            // �����Խ�����������Դ�ͷŻ����ò�����ȷ�� UI �������ڱ�������ٱ����κ�״̬������

            // ����ÿ�ʵ��
            instance = null;
        }
        /// <summary>
        /// ��ȡָ����������������
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="childName"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public T GetChildComponent<T>(UIPanelType type, string childName)
        {
            if (panelDict[type] == null)
            {
                throw new Exception("Error��Cannot Find This component");

            }
            return panelDict[type].transform.Find(childName).GetComponent<T>();
        }


        /// <summary>
        /// ������ ��ȡ�����������GameObject����
        /// </summary>
        /// <param name="type">UIPanelType</param>
        /// <param name="childName"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public GameObject GetChild(UIPanelType type, string childName)
        {
            if (panelDict[type] == null)
            {
                throw new Exception("Error��Cannot Find This child");

            }
            return panelDict[type].transform.Find(childName).gameObject;
        }
        /// <summary>
        /// ������ ��ȡ�����������Recttransform����
        /// </summary>
        /// <param name="type"></param>
        /// <param name="childName"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public RectTransform GetChildRectTransform(UIPanelType type, string childName)
        {
            if (panelDict[type] == null)
            {
                throw new Exception("Error��Cannot Find This child");

            }
            return panelDict[type].transform.Find(childName).transform.GetComponent<RectTransform>();
        }
 
    }
}
