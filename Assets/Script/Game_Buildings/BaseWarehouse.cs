using Assets.Script.Resourse;
using Resource;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Building
{
    public abstract class BaseWarehouse : MonoBehaviour
    {
        [SerializeField] private ResourceWarhouse[] _resourcesPlaces;
        [SerializeField] private float timeToCreateRes;

        private Dictionary<EnumResource, ResourceWarhouse> resWarhouses = new Dictionary<EnumResource, ResourceWarhouse>();
        private Dictionary<Type, BuildingState> behaviorsMap;
        private BuildingState currentState;

        private ResourceSpawner resourceSpawner;
        [Inject]
        private void Construct(ResourceSpawner resourceSpawner)
        {
            this.resourceSpawner = resourceSpawner;
        }
        public void GetInventory()
        {

        }

        protected void Start()
        {
            InitResWarhouses();
            InitBehaviors();
            SetBehaviorAfterBuild();
        }

        private void Update()
        {
       
        }
        public void InitBehaviors()
        {
            behaviorsMap = new Dictionary<Type, BuildingState>();

            behaviorsMap[typeof(BeforeConstructState)] = new BeforeConstructState(resWarhouses[EnumResource.Log]);

            behaviorsMap[typeof(AfterConstructState)] = new AfterConstructState(
                    resWarhouses[EnumResource.Money], 
                    resWarhouses[EnumResource.Money].myTransform.position, 
                    timeToCreateRes, resourceSpawner);

            SetBehaviorBeforeBuild();
        }
        public void InitResWarhouses()
        {
            for(int i = 0; i< _resourcesPlaces.Length; i++)
                resWarhouses.Add(_resourcesPlaces[i].EnumResource, _resourcesPlaces[i]);
        }

        protected void SetBehavior(BuildingState animationBehavior)
        {
            if (currentState != null)
                currentState.Exit();

            currentState = animationBehavior;
            currentState.Enter();
        }

        protected BuildingState GetBehavior<T>() where T : BuildingState
        {
            var type = typeof(T);
            return behaviorsMap[type];
        }

        protected void SetBehaviorBeforeBuild()
        {
            var behavior = GetBehavior<BeforeConstructState>();
            SetBehavior(behavior);
        }

        protected void SetBehaviorAfterBuild()
        {
            var behavior = GetBehavior<AfterConstructState>();
            SetBehavior(behavior);
        }
    }
}