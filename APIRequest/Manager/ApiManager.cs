using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using XJFramework.ApiRequest;

public enum RequestType
{
    POST,
    GET,
    DELETE,
    PUT,
}
/// <summary>
/// api���󹤾���
/// </summary>
public class ApiManager : Singleton<ApiManager>
{
    private Dictionary<ApiType, string> ApiUrlDic;
    private string ApiConfigLoad;
    public ApiManager()
    {
        ApiUrlDic = new Dictionary<ApiType, string>();

        ApiConfigLoad = "AssetBundles/StandaloneWindows/apiconfiguration";

        LoadApiConfig();
    }
    /// <summary>
    /// ����api�ӿ�
    /// </summary>
    private void LoadApiConfig()
    {
        TextAsset textAsset = new TextAsset();
        textAsset = (TextAsset) AssetBundle.LoadFromFile(ApiConfigLoad).LoadAsset("ApiInfo");
        ApiInfoList desieial = JsonConvert.DeserializeObject<ApiInfoList>(textAsset.text);
        foreach(ApiInfo info in desieial.Info) 
        {
            ApiUrlDic.Add(info.ApiType, info.APIRoute);
        }
    }
    /// <summary>
    /// ����һ��WebRequest
    /// </summary>
    /// <param name="type"></param>
    /// <param name="postJson"></param>
    /// <param name="requestType"></param>
    /// <returns></returns>
    public UnityWebRequest SetWebRequest(ApiType type, string postJson, RequestType requestType)
    {
        switch (requestType)
        {
            case RequestType.POST:
                UnityWebRequest unityWebRequestpost = new UnityWebRequest(ApiUrlDic[type],"POST");
                unityWebRequestpost.SetRequestHeader("Content-Type", "application/json");
                byte[] bodyRawPost = System.Text.Encoding.UTF8.GetBytes(postJson);
                unityWebRequestpost.uploadHandler = new UploadHandlerRaw(bodyRawPost);
                unityWebRequestpost.downloadHandler = new DownloadHandlerBuffer();
                return unityWebRequestpost;
            case RequestType.GET:
                UnityWebRequest unityWebRequestget = new UnityWebRequest(ApiUrlDic[type], "GET");
                unityWebRequestget.SetRequestHeader("Content-Type", "application/json");
                byte[] bodyRawGet = System.Text.Encoding.UTF8.GetBytes(postJson);
                unityWebRequestget.uploadHandler = new UploadHandlerRaw(bodyRawGet);
                unityWebRequestget.downloadHandler = new DownloadHandlerBuffer();
                return unityWebRequestget;
            case RequestType.DELETE:
                UnityWebRequest unityWebRequestdelete = new UnityWebRequest(ApiUrlDic[type],"DELETE");
                unityWebRequestdelete.SetRequestHeader("Content-Type", "application/json");
                byte[] bodyRawDelete = System.Text.Encoding.UTF8.GetBytes(postJson);
                unityWebRequestdelete.uploadHandler = new UploadHandlerRaw(bodyRawDelete);
                unityWebRequestdelete.downloadHandler = new DownloadHandlerBuffer();
                return unityWebRequestdelete;
            case RequestType.PUT:
                UnityWebRequest unityWebRequestPut = new UnityWebRequest(ApiUrlDic[type],"PUT");
                unityWebRequestPut.SetRequestHeader("Content-Type", "application/json");
                byte[] bodyRawPut = System.Text.Encoding.UTF8.GetBytes(postJson);
                unityWebRequestPut.uploadHandler = new UploadHandlerRaw(bodyRawPut);
                unityWebRequestPut.downloadHandler = new DownloadHandlerBuffer();
                return unityWebRequestPut;
        }
        Debug.LogError("����ֵΪ��");
        return null;
    }
    /// <summary>
    /// ת�����������ʵ����
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public string FromEntityToJson(Dictionary<string,object> t)
    {
        string EntityJson = JsonConvert.SerializeObject(t);

        return EntityJson;
    }
    /// <summary>
    /// ������λ���������ŵ�json�ַ�����������hashtable
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="jsonData"></param>
    /// <returns></returns>
    public Dictionary<string,object> ParseJsonDataToDic(string jsonData)
    {

        if(!string.IsNullOrEmpty(jsonData))
        {
            Dictionary<string,object> entityTable = new Dictionary<string,object>();

            JObject jo = (JObject)JsonConvert.DeserializeObject(jsonData);
            foreach(var item in jo)
            {
                entityTable.Add(item.Key, item.Value);
            }
            return entityTable;
        }
        else
        {
            Debug.LogError("json����Ϊ��");
            return null;
        }
    }
    /// <summary>
    /// ֱ�ӷ���api���󣬲��ҷ��ؽ����õ�����.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="postJson"></param>
    /// <param name="requestType"></param>
    /// <returns></returns>
    public async UniTask<string> GetApiResponse(ApiType type, string postJson, RequestType requestType)
    {
        UnityWebRequest request = SetWebRequest(type, postJson, requestType);
        await request.SendWebRequest();
        if(request.error != null)
        {
            Debug.LogError(request.error.ToString());

        }
        else if(request.isDone)
        {
            string downloadjsonData = request.downloadHandler.text;
            
            request.Dispose();

            return downloadjsonData;
        }
        else
        { 
            Debug.LogError("downloadHandleΪ��"); 
        }
        return null;
    }
    /// <summary>
    /// ��json���ݽ�������ʵ������
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="jsonData"></param>
    /// <returns></returns>
    public T ParseJsonDataToEntity<T>(string jsonData)
    {
        T entity = JsonConvert.DeserializeObject<T>(jsonData);
        return entity;
    }
}
