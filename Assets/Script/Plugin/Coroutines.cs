using System.Collections;
using UnityEngine;

namespace Assets.Script
{
    public sealed class Coroutines : MonoBehaviour
    {
        public static Coroutines instance
        {
            get
            {
                if (m_instance == null)
                {
                    var go = new GameObject("[COROUTINE MANAGER]");
                    m_instance = go.AddComponent<Coroutines>();
                    DontDestroyOnLoad(go);
                }

                return m_instance;
            }
        }

        private static Coroutines m_instance;

        public static Coroutine StartRoutine(IEnumerator enumerator)
        {
            return instance.StartCoroutine(enumerator);
        }

        public static void StopRoutine(Coroutine coroutine)
        {
            instance.StopCoroutine(coroutine);
        }

        public static void StopRoutine(IEnumerator coroutine)
        {
            instance.StopCoroutine(coroutine);
        }
    }
}
