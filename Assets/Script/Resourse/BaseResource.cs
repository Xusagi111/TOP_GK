using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UniRx;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Resource
{
    public abstract class BaseResource : MonoBehaviour
    {
        public Image Icon;
        [field: SerializeField] public EnumResource TypeRes { get; protected set; } = EnumResource.NullType;

        public readonly ReactiveCommand resourceInsert;
        private CompositeDisposable _disposable = new CompositeDisposable();
        public CompositeDisposable Disposable => _disposable;

        protected float anim = 5f;



        protected async UniTaskVoid JumpToPoint(Vector3 startPoint, Transform endTransf, float offsetY, float height)
        {
            transform.parent = null;

            var expiredSeconds = 0f;
            var progress = 0f;
            Vector3 endPos = Vector3.zero;

            while (progress < 1)
            {
                float deltaTime = Time.deltaTime;

                await UniTask.SwitchToThreadPool();
                expiredSeconds += deltaTime;
                progress = expiredSeconds / anim;
                await UniTask.SwitchToMainThread();

                endPos = new Vector3(endTransf.position.x, endTransf.position.y + offsetY, endTransf.position.z);
                transform.position = MathfParabola.Parabola(startPoint, endPos, height, progress);

                await UniTask.Yield();
            }
            resourceInsert.Execute();
            _disposable.Clear();
            transform.SetParent(endTransf);
        }

        public void JumpFromToTransform(Vector3 startPoint, Transform endTransf, float offsetY, float height) => 
            JumpToPoint(startPoint, endTransf, offsetY, height).Forget();

        private void OnDisable()
        {
            DisposableExtension.Dispose(_disposable);
        }

        public class Factory : PlaceholderFactory<EnumResource, Vector3, BaseResource> { }
    }
}