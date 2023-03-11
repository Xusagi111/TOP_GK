using Cysharp.Threading.Tasks;
using Resource;
using System;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Building
{
    [Serializable]
    public class ResourceWarhouse
    {
        [SerializeField] private int maxElement = 10;
        [SerializeField] private EnumResource typeRes;
        [SerializeField] private Collider myCollider;

        private int curCount;
        public Transform myTransform => myCollider.transform;
        private List<BaseResource> _resources = new List<BaseResource>();
        private CompositeDisposable _dispose = new CompositeDisposable();

        #region Props
        public List<BaseResource> AllResources => _resources;
        public EnumResource EnumResource => typeRes;
        public Collider Collider => myCollider;
        public int MaxElement => maxElement;

        public bool isStack { get{
                return curCount < maxElement;
            }}
        #endregion

        public void Init()
        {
            myCollider.OnTriggerEnterAsObservable().Subscribe(_ =>
            {

            }).AddTo(_dispose);
        }
        public void AddResource(BaseResource resource) {
            if (curCount < MaxElement)
            {
                _resources.Add(resource);
                curCount++;
            }
        } 
        public void RemoveResource(BaseResource resource)
        {
            _resources.Remove(resource);
            curCount--;
        }

        public void RemoveResources(int countResource) => _resources.RemoveRange(0, countResource);

        public void SendResourcesTo(int countRes, Vector3 startPos, ResourceWarhouse endTransform) =>
            SendResourcesTask(countRes, startPos, endTransform).Forget();

        private async UniTaskVoid SendResourcesTask(int countRes, Vector3 startPos, ResourceWarhouse resWarhouse)
        {
            float offsetY = 0;
            for(int i = 0; i< countRes; i++)
            {
                _resources[i].JumpFromToTransform(startPos, resWarhouse.myTransform, offsetY, 2);

                RemoveResource(_resources[i]);
                resWarhouse.SubscribeOnResourceInsert(_resources[i]);

                offsetY += 1;
                await UniTask.Delay(TimeSpan.FromMilliseconds(100));
            }
        }

        public void SubscribeOnResourceInsert(BaseResource resource)
        {
            resource.resourceInsert.Single()
                .Subscribe(_ => AddResource(resource))
                .AddTo(resource.Disposable);
        }
    }
}