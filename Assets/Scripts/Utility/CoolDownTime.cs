using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CharacterEditorPackage.Code
{
    /// <summary>
    /// 冷却时间
    /// </summary>
    public class CoolDownTime
    {
        /// <summary>
        /// 是否冷却完成
        /// </summary>
        public bool isReady = true;
        public int Interval = 100;


        public CoolDownTime(int interval)
        {
            Interval = interval;
        }
        /// <summary>
        /// 开始冷却
        /// </summary>
        public async UniTaskVoid Start()
        {
            isReady = false;
            await UniTask.Delay(Interval);
            isReady = true;
        }


    }
}