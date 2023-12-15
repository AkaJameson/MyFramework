using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleLogin
{
    public class LoginSetting
    {
     

        /// <summary>
        /// ±£¥Ê’À∫≈√‹¬Î
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="loginPassword"></param>
        public static void SettingEnterCode(string loginName, string loginPassword)
        {
            PlayerPrefs.SetString(loginName, loginPassword);
        }
        /// <summary>
        /// √‹¬Î¬ﬂº≠≈–∂œ
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="loginPassword"></param>
        /// <returns></returns>
        public static bool GetUserNameAndPasswordProof(string loginName, string loginPassword)
        {
            if (PlayerPrefs.GetString(loginName).Equals(loginPassword))
            {
                return true;
            }
            else return false;
        }
        /// <summary>
        /// ∏¸∏ƒ√‹¬Î
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="preLoginPassword"></param>
        /// <param name="newLoginPassword"></param>
        /// <returns></returns>
        public static bool ChangePasswordInfo(string loginName, string preLoginPassword, string newLoginPassword)
        {
            if (PlayerPrefs.GetString(loginName).Equals(preLoginPassword))
            {
                PlayerPrefs.SetString(loginName, newLoginPassword);
                return true;
            }
            else return false;
        }
    }
}
