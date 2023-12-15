using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XJFramework.ApiRequest
{
    [Serializable]
    public class ApiInfo
    {
        // Api的名字
        public ApiType ApiType;
        //Api的路径
        public string APIRoute;
    }
    [Serializable]
    public class ApiInfoList
    {
        public ApiInfo[] Info;
    }
}
