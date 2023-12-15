using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Newtonsoft.Json;
using System;
namespace XJFramework.UIFramework
{
    /// <summary>
    /// 这个类仍然不完善，在切换场景是没有添加保存上个场景信息的方法，当需要重新返回上个场景的时候需要重新写一遍加载，不完善，
    /// 并且当前的clearmanager相当的粗放，造成了不必要的cpu计算开销.
    /// 每个basepanel没有自己panel的姓名TODO。
    /// </summary>
    public class UIManager
    {
        /// <summary>
        /// UIManager单例
        /// </summary>
        private static UIManager instance;
        /// <summary>
        /// 保存Panel的姓名
        /// </summary>
        private Dictionary<UIPanelType, string> panelPathDict;
        /// <summary>
        ///保存所有实例化面板的游戏物体身上的BasePanel
        /// </summary>
        private Dictionary<UIPanelType, BasePanel> panelDict;
        /// <summary>
        /// 保存ui的层级结构
        /// </summary>
        private Stack<BasePanel> panelStack;
        /// <summary>
        /// 保存上个面板的panel信息 TODO
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
        /// 保存解析完成的实体类文件
        /// </summary>
        private ConfigInfo panelListJson;
        /// <summary>
        /// 保存面板的路径信息
        /// </summary>

        //保存uipanel的ab包
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
                //如果找不到，那么就找到这个面板的prefab路径，然后根据prefab去实例化面板
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
        /// 读取配置文件
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
        /// 将读取到的panel信息加载到UIManager的字典中
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
        /// 把某个页面入栈，把某个页面显示在页面上，并且触发每个页面的OnEnter方法
        /// </summary>
        public void PushPanel(UIPanelType panelType)
        {
            if (panelStack == null)
                panelStack = new Stack<BasePanel>();
            //判断一下栈里面是否有页面,如果有，启动栈中面板的pause()方法
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
        /// 把某个页面出栈，关闭页面的显示
        /// </summary>
        public void PopPanel()
        {
            if (panelStack == null)
                panelStack = new Stack<BasePanel>();
            if (panelStack.Count <= 0) { return; }
            //关闭栈顶页面的显示
            BasePanel topPanel = panelStack.Pop();
            topPanel.OnExit();

            if (panelStack.Count <= 0) { return; }
            BasePanel topPanel2 = panelStack.Peek();
            topPanel2.OnResume();

        }
        /// <summary>
        /// 工具类 获取panel下面对象的组件
        /// </summary>
        /// <typeparam name="T">UIPanelType</typeparam>
        /// <param name="type"></param>
        /// <param name="childName"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>

        /// <summary>
        /// 查看栈顶面板
        /// </summary>
        /// <returns></returns>
        public BasePanel PeekPanel()
        {
            return panelStack.Peek();
        }
        /// <summary>
        /// 队列管理ui面板入队操作
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
        /// 队列管理ui面板出队操作
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
        /// 查询队伍数量
        /// </summary>
        /// <returns></returns>
        public int CountQuene()
        {
            return noneInStackPanelQuene.Count;
        }

        /// <summary>
        /// 重置UI管理器
        /// </summary>
        public void ClearUIManager()
        {
            // 清除面板字典和其他资源，确保所有 UI 相关的资源都被释放或清除
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

            // 释放 AssetBundle
            if (instance.PanelFileAB != null)
            {
                instance.PanelFileAB.Unload(true);
                instance.PanelFileAB = null;
            }

            // 还可以进行其他的资源释放或重置操作，确保 UI 管理器在被清除后不再保留任何状态或引用

            // 最后置空实例
            instance = null;
        }
        /// <summary>
        /// 获取指定面板的子物体的组件
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
                throw new Exception("Error，Cannot Find This component");

            }
            return panelDict[type].transform.Find(childName).GetComponent<T>();
        }


        /// <summary>
        /// 工具类 获取面板下面对象的GameObject属性
        /// </summary>
        /// <param name="type">UIPanelType</param>
        /// <param name="childName"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public GameObject GetChild(UIPanelType type, string childName)
        {
            if (panelDict[type] == null)
            {
                throw new Exception("Error，Cannot Find This child");

            }
            return panelDict[type].transform.Find(childName).gameObject;
        }
        /// <summary>
        /// 工具类 获取面板下面对象的Recttransform属性
        /// </summary>
        /// <param name="type"></param>
        /// <param name="childName"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public RectTransform GetChildRectTransform(UIPanelType type, string childName)
        {
            if (panelDict[type] == null)
            {
                throw new Exception("Error，Cannot Find This child");

            }
            return panelDict[type].transform.Find(childName).transform.GetComponent<RectTransform>();
        }
 
    }
}
