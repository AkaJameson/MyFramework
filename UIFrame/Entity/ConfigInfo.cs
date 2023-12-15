using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace XJFramework.UIFramework
{
    [SerializeField]
    public class ConfigInfo
    {
        public PanelPathInfo[] PanelList { get; set; }

        public string Path { get; set; }
    }
}
