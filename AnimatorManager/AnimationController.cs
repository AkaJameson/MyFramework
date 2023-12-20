using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace XJFramework.AnimationManager
{
    public class AnimationManager : Singleton<AnimationManager>
    {
        private Dictionary<string, Animator> animatorDic;

        public AnimationManager()
        {

            animatorDic = new Dictionary<string, Animator>();

        }
        /// <summary>
        /// ���Animator����������
        /// </summary>
        /// <param name="animatorName"></param>
        /// <param name="animator"></param>
        public void AddAnimator(string animatorName, Animator animator)
        {
            if (!animatorDic.ContainsKey(animatorName))
            {
                animatorDic.Add(animatorName, animator);
            }
            else
            {
                Debug.LogWarning("animator with the same name already exists in the dictionary");
            }
        }
        /// <summary>
        /// �ӹ��������Ƴ�ָ����animator
        /// </summary>
        /// <param name="animatorName"></param>
        public void RemoveAnimator(string animatorName)
        {
            if (animatorDic.ContainsKey(animatorName))
            {
                animatorDic.Remove(animatorName);
            }
            else
            {
                Debug.LogWarning("Dic Not Found the Animator with the same Name");
            }
        }
        /// <summary>
        /// ����ָ���������Ͷ�������ģʽ
        /// </summary>
        /// <param name="animatorName"></param>
        /// <param name="animationName"></param>
        /// <param name="mode">1.ѭ������2.���ź���ͣ</param>
        public void PlayAnimation(string animatorName, string animationName, int layer)
        {
            animatorDic.TryGet(animatorName).Play(animationName, layer, 0);
        }
        /// <summary>
        /// ��ͣ����
        /// </summary>
        /// <param name="animatorName"></param>
        public void StopAnimationPlay(string animatorName)
        {
            animatorDic.TryGet(animatorName).speed = 0;
        }
        /// <summary>
        /// ���ö��������ٶ�
        /// </summary>
        /// <param name="animatorName"></param>
        /// <param name="speed"></param>
        public void SetAnimationPlaySpeed(string animatorName, float speed)
        {
            animatorDic.TryGet(animatorName).speed = speed;
        }

        public void ReadAnimationList()
        {
            foreach(var animator in animatorDic)
            {
                Debug.Log(animator.Key.ToString() + " " + animator.Value.name);
            }
        }
        /// <summary>
        /// �����ֵ��е�animator
        /// </summary>
        /// <param name="animatorName"></param>
        /// <returns></returns>
        public bool SearchAnimatorIfInDic(string animatorName)
        {
            if (!animatorDic.ContainsKey(animatorName))
            {
                return false;
            }
            else { return true; }
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="animatorName"></param>
        /// <returns></returns>
        public bool SearchAnimatorIfInDic(string animatorName,out Animator animator)
        {
            if (!animatorDic.ContainsKey(animatorName))
            {
                animator = null;
                return false;
            }
            else { animator = animatorDic[animatorName]; return true; }
        }
    }
}
