using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System.Net;
using System;
using Resource;

namespace Resourse
{
    public abstract class BaseResourse : MonoBehaviour
    {
        public Image Icon;
        // ---------------------

        protected float anim = 2f;

        [field: SerializeField] public EnumResource TypeRes { get; protected set; } = EnumResource.NullType;

        protected IEnumerator JumpToPoint(Vector3 startPoint, Transform endTransf, float height)
        {

            var expiredSeconds = 0f;
            var progress = 0f;

            while (progress < anim)
            {
                expiredSeconds += Time.deltaTime;
                progress = expiredSeconds / anim;

                transform.position = MathfParabola.Parabola(startPoint, endTransf.position, height, progress);

                yield return null;
            }

            yield break;
        }

        public void Jump(Vector3 startPoint, Transform endTransf, float height) => 
            JumpToPoint(startPoint, endTransf, height).ToObservable().Subscribe();
    }
}