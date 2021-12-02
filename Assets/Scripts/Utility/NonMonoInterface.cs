using System.Collections;
using UnityEngine.Events;

namespace CharacterEditorPackage.Code.Utility
{
    /// <summary>
    /// 非monobeheviour类使用
    /// </summary>
    public class NonMonoInterface: MonoSingleton<NonMonoInterface>
    {
        public event UnityAction OnStart;
        public event UnityAction OnUpdate;

        public void RunCoroutine(IEnumerator enumerator)
        {
            StartCoroutine(enumerator);
        }
    }
}