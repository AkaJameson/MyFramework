using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace XJFramework.SoundManager
{
public enum SoundLoadModel
{
    Resources,
    AssetBundle
}
    public static class Global
    {
        /// <summary>
        /// resource �����
        /// </summary>
        public static string BGM = "Sound/BGM";
        public static string ClickSound = "Sound/Click";




        /// <summary>
        ///AssetBoundle���� 
        /// </summary>
        public static string AssetBundleLoadPath = "";


    }
}
