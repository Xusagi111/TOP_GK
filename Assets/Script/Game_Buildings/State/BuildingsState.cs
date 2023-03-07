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
        public DataBulding DataBuilding { get; private set; }
        private void Awake()
        {
            DataBuilding = GetComponent<DataBulding>();
            InitBuildings();
        }

        private void InitBuildings()
        {
            //Прокидывания _dataBuilding не подходят
            behaviorMap = new Dictionary<Type, StateBuilbing>();
            behaviorMap[typeof(ConstructionBuilding<Log>)] = new ConstructionBuilding<Log>(DataBuilding);
            behaviorMap[typeof(ConstructionBuilding<Log>)] = new ConstructionBuilding<Log>(DataBuilding);
            behaviorMap[typeof(ConditionBuilding<MoneyObj>)] = new ConditionBuilding<MoneyObj>(DataBuilding);
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

        public void SetConstruct<T>()
        {
            DataBuilding.ConstructViewBuilding();
            var behavior = GetBuilding<ConstructionBuilding<T>>();
            SetBuilding(behavior);
        }

        public void SetCreateRes<AddResource, GetResource>(Collider TriggerContact)
        {
            DataBuilding.EndViewFactory();
            var behavior = GetBuilding<ConditionBuilding<AddResource, GetResource>>();
            SetBuilding(behavior);
        }

        public void SetCreateResNoAddResource<GetResource>()
        {
            DataBuilding.EndViewCreatingIcomeBuildings();
            var behavior = GetBuilding<ConditionBuilding<GetResource>>();
            SetBuilding(behavior);
        }
    }
}