using Building;
using Resource;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Script.Game_Buildings.State
{
    public abstract class BaseBuildingsState<AddRes, GetRes> : MonoBehaviour
    {
        private Dictionary<Type, StateBaseBuilbing> behaviorMap;
        private IBuildingState _ICurrentState;
        public DataBulding DataBuilding { get; private set; }

        [Inject]
        private UpdateTimeCreateR _updateTimeCreateR;
        private bool _isinit = false;

        private void Awake() => DataBuilding = GetComponent<DataBulding>();
        private void Start() => InitBuildings();

        private void InitBuildings()
        {
            if (_isinit == true) return;

            Debug.Log(_updateTimeCreateR);

            //Прокидывания _dataBuilding не подходят
            behaviorMap = new Dictionary<Type, StateBaseBuilbing>();
            behaviorMap[typeof(ConstructionBuilding<AddRes, GetRes>)] = new ConstructionBuilding<AddRes, GetRes>(DataBuilding, this);
            behaviorMap[typeof(StateBuildingCreateRes<AddRes, GetRes>)] = new StateBuildingCreateRes<AddRes, GetRes>(DataBuilding, _updateTimeCreateR);
            behaviorMap[typeof(StateBuildingCreateRes<GetRes>)] = new StateBuildingCreateRes<GetRes>(DataBuilding);
            behaviorMap[typeof(StateBuildingCreateRes<MoneyObj>)] = new StateBuildingCreateRes<MoneyObj>(DataBuilding); //Возможно заменить

            _isinit = true;
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
            if (_isinit == false) InitBuildings();

            var type = typeof(T);
            foreach (var item in behaviorMap)
            {
                var a = item.Value;
            }
            return behaviorMap[type];
        }

        public void SetConstruct()
        {
            DataBuilding.ConstructViewBuilding();
            var behavior = GetBuilding<ConstructionBuilding<AddRes, GetRes>>();
            SetBuilding(behavior);
        }

        public void SetCreateRes()
        {
            DataBuilding.EndViewFactory();
            var behavior = GetBuilding<StateBuildingCreateRes<AddRes, GetRes>>();
            SetBuilding(behavior);
        }

        public void SetCreateResNoAddResource()
        {
            DataBuilding.EndViewCreatingIcomeBuildings();
            var behavior = GetBuilding<StateBuildingCreateRes<GetRes>>();
            SetBuilding(behavior);
        }

        public void Update()
        {
            if (_ICurrentState != null) _ICurrentState.IUpdate();
        }
    }
}