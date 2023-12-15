using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XJFramework.ApiRequest
{
    [Serializable]
    public class ApiInfo
    {
        // Api������
        public ApiType ApiType;
        //Api��·��
        public string APIRoute;
    }
    [Serializable]
    public class ApiInfoList
    {
        public ApiInfo[] Info;
    }
}
