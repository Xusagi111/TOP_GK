using Building;
using Resource;
using System.Collections;
using UnityEngine;

namespace Assets.Script.Game_Buildings.State
{
    public class ConditionBuilding<GetResource> : StateBuilbing
    {
        protected EnumResource CurrentGetTypeRes;
        private Collider _triggerContact;
        protected BaseWarehouse GetRes;


        public ConditionBuilding(DataBulding dataBulding)
        {
            CurrentGetTypeRes = GetTypeBuilding.GetTypeRes(typeof(GetResource));
            dataBulding.EndViewCreatingIcomeBuildings(); //Возможно перемещение в  Enter and Exit
        }
    }

    public class ConditionBuilding<AddResource, GetResource> : StateBuilbing
    {
        protected EnumResource CurrentGetTypeRes;
        private Collider _addResTrigger;
        private Collider _getResTrigger;
        private float TimeCreateOneElement;
        protected BaseWarehouse GetRes;
        private IEnumerator _updateTimeCreateR;

        public ConditionBuilding(DataBulding dataBulding)
        {
            CurrentTypeRes = GetTypeBuilding.GetTypeRes(typeof(AddResource));
            CurrentGetTypeRes = GetTypeBuilding.GetTypeRes(typeof(GetResource));
            _addResTrigger = dataBulding.AddRes;
            _getResTrigger = dataBulding.GetRes;
        }

        public override void Enter()
        {
            DataBulding.EndViewFactory();
            IsUpdateTike = true;

            //подписать на событие получения рес   _dataBulding.ConstructionBulding. 
        }

        public override void Exit()
        {
            DataBulding.DisableView();
            IsUpdateTike = false;

            //отписать от события  _dataBulding.ConstructionBulding. 
        }

        public override void Tick()
        {
            if (IsUpdateTike == false) return;

            if (_updateTimeCreateR == null && 
                BaseWarehouse.AllGameObj.Count > 1 &&
                GetRes.AllResorce.MaxElement > BaseWarehouse .AllGameObj.Count)
            {
                CreateRes();
            }
        }

        public void CreateRes()
        {
            _updateTimeCreateR = UpdateTimeCreateR.UpdateTime(BaseWarehouse, GetRes.AllResorce.AllGameObj, BaseWarehouse.TypeRes, TimeCreateOneElement, TimeCreateOneResourceT);
            Coroutines.instance.StartCoroutine(_updateTimeCreateR);
        }
    }
}
