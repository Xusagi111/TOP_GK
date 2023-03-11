using Building;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Script.Resourse
{
    public class AfterConstructState : BuildingState
    {
        private Vector3 position;
        private float timeForCreateRes;

        private readonly ResourceSpawner resourceSpawner;
        public AfterConstructState(ResourceWarhouse resourceWarhouse, Vector3 position, float timeForCreateRes, ResourceSpawner resourceSpawner) : base(resourceWarhouse)
        {
            this.position = position;
            this.timeForCreateRes= timeForCreateRes;
            this.resourceSpawner = resourceSpawner;
        }

        public override void Enter()
        {
            CreateResource().Forget();
        }

        public override void Run()
        {
            
        }
        private async UniTaskVoid CreateResource()
        {
            while(!isExit)
            {
                resourceSpawner.Create(resourceWarhouse.EnumResource, position);
                await UniTask.Delay(TimeSpan.FromSeconds(timeForCreateRes));
            }
        }
    }
}
