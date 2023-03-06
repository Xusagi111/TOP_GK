using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UniRx;

namespace Resource
{
    public class BaseResource : MonoBehaviour
    {
        public Image Icon;
        [field: SerializeField] public EnumResource TypeRes { get; protected set; } = EnumResource.NullType;

        protected float anim = 2f;



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