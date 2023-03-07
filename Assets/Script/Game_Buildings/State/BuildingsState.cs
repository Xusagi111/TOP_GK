using Building;
using Resource;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.Game_Buildings.State
{

    public class BuildingsState : MonoBehaviour
    {
        private Dictionary<Type, StateBuilbing> behaviorMap;
        private IBuildingState _ICurrentState;
        [field: SerializeField] private DataBulding _dataBuilding;
        private void Awake()
        {
            InitBuildings();
        }

        private void InitBuildings()
        {
            //Прокидывания _dataBuilding не подходят
            behaviorMap = new Dictionary<Type, StateBuilbing>();
            behaviorMap[typeof(ConstructionBuilding<Log>)] = new ConstructionBuilding<Log>(_dataBuilding);
            behaviorMap[typeof(ConstructionBuilding<Log>)] = new ConstructionBuilding<Log>(_dataBuilding);
            behaviorMap[typeof(ConditionBuilding<MoneyObj>)] = new ConditionBuilding<MoneyObj>(_dataBuilding);
        }

        protected void SetBuilding(IBuildingState House)
        {
            if (_ICurrentState != null)
                _ICurrentState.Exit();

            _ICurrentState = House;
            _ICurrentState.Enter();
        }

        protected IBuildingState GetBuilding<T>() where T : IBuildingState
        {
            var type = typeof(T);
            return behaviorMap[type];
        }

        protected void SetConstruct<T>()
        {
            _dataBuilding.ConstructViewBuilding();
            var behavior = GetBuilding<ConstructionBuilding<T>>();
            SetBuilding(behavior);
        }

        protected void SetCreateRes<AddResource, GetResource>(Collider TriggerContact)
        {
            _dataBuilding.EndViewFactory();
            var behavior = GetBuilding<ConditionBuilding<AddResource, GetResource>>();
            SetBuilding(behavior);
        }

        protected void SetCreateResNoAddResource<GetResource>()
        {
            _dataBuilding.EndViewCreatingIcomeBuildings();
            var behavior = GetBuilding<ConditionBuilding<GetResource>>();
            SetBuilding(behavior);
        }
    }
}