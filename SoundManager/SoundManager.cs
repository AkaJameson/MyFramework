using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
namespace XJFramework.SoundManager
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : MonoBehaviour
    {
        public SoundLoadModel mode;
        public static bool isPlay = false;
        private AssetBundle ab;
        public AudioSource audioSource;
        private Dictionary<string, AudioClip> clipDic;
        private float volume;
        public static SoundManager Instance;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject.transform.root);

            audioSource = GetComponent<AudioSource>();
            clipDic = new Dictionary<string, AudioClip>();
            if (mode == SoundLoadModel.AssetBundle)
            {
                ab = AssetBundle.LoadFromFile(Global.AssetBundleLoadPath);
            }
        }
        /// <summary>
        /// Resource类加载音频Clip
        /// </summary>
        /// <param name="path"></param>
        /// <param name="clip"></param>
        private AudioClip LoadResources(string path)
        {

            return (AudioClip)Resources.Load(path);

        }
        /// <summary>
        /// AssetBundle加载音频Clip
        /// </summary>
        /// <param name="assetName"></param>
        /// <returns></returns>
        private AudioClip LoadResourcesAssetBundle(string assetName)
        {
            return (AudioClip)ab.LoadAsset(assetName);
        }

        /// <summary>
        /// 获取AudioClip ,通过assetBundle 或者Resource加载
        /// </summary>
        /// <param name="assetNameorPath">Resoure类加载填入Path,AssetBundle加载填入AssetName</param>
        /// <returns></returns>
        private AudioClip GetAudioClip(string assetNameorPath)
        {
            if (mode != SoundLoadModel.AssetBundle)
            {
                if (!clipDic.ContainsKey(assetNameorPath))
                {
                    AudioClip clip = LoadResources(assetNameorPath);
                    clipDic.Add(assetNameorPath, clip);
                    return clip;
                }
                else return clipDic[assetNameorPath];
            }
            else
            {
                if (!clipDic.ContainsKey(assetNameorPath))
                {
                    AudioClip clip = LoadResourcesAssetBundle(assetNameorPath);
                    clipDic.Add(assetNameorPath, clip);
                    return clip;
                }
                else return clipDic[assetNameorPath];
            }
        }
        /// <summary>
        /// 播放Bgm
        /// </summary>
        /// <param name="assetNameofPath">Resoure类加载填入Path,AssetBundle加载填入AssetName</param>
        public void PlayBGM(string assetNameorPath, float volume = 1.0f)
        {
            audioSource.clip = GetAudioClip(assetNameorPath);
            audioSource.loop = true;
            audioSource.volume = 1.0f;
            audioSource.volume = volume;
            isPlay = true;
            audioSource.Play();
        }
        /// <summary>
        /// 关闭播放BGM isplay是标志位
        /// </summary>

        public void StopBGM()
        {
            isPlay = false;
            audioSource.Stop();
        }
        /// <summary>
        /// 
        /// </summary>
        public void ChangeBGMVolume()
        {
            audioSource.volume = volume;
        }
        public void PlayOneShoot(string assetNameorPath, float volume = 1.0f)
        {
            audioSource.PlayOneShot(GetAudioClip(assetNameorPath), volume);
        }
    }
}
