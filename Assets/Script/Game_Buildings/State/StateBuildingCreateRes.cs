using Building;
using Resource;
using System.Collections;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Assets.Script.Game_Buildings.State
{
    [System.Serializable]
    public class StateBuildingCreateRes<GetResource> : StateBaseBuilbing
    {
        protected EnumResource CurrentGetTypeRes;
        protected BaseWarehouse GetRes;
        private float TimeCreateOneElement;
        private IEnumerator _updateTimeCreateR;
        private Collider _getResTrigger;

        public StateBuildingCreateRes(DataBulding dataBulding)
        {
            CurrentGetTypeRes = GetTypeBuilding.GetTypeRes(typeof(GetResource));
            dataBulding.EndViewCreatingIcomeBuildings(); //Возможно перемещение в  Enter and Exit
            _getResTrigger = dataBulding.GetRes;
        }

        public override void Enter()
        {
            DataBulding.EndViewFactory();
            IsUpdateTike = true;

            _getResTrigger.OnTriggerEnterAsObservable()
            .Subscribe(collider => InventoryContact.GetCollizionRes(collider.gameObject, GetRes.AllResorce, _getResTrigger.transform))
            .AddTo(Disposable);
        }

        public override void FixedTick()
        {
            if (IsUpdateTike == false) return;

            if (_updateTimeCreateR == null &&
                BaseWarehouse.AllGameObj.Count > 1 &&
                GetRes.AllResorce.MaxElement > BaseWarehouse.AllGameObj.Count)
            {
                CreateRes();
            }
        }

        public void CreateRes()
        {
            _updateTimeCreateR = UpdateTimeCreateR.UpdateTime(BaseWarehouse, GetRes.AllResorce.AllGameObj, 
                BaseWarehouse.TypeRes, TimeCreateOneElement, DataBulding.TimeCreateOneResourceT);
            Coroutines.instance.StartCoroutine(_updateTimeCreateR);
        }

        public override void Exit()
        {
            DataBulding.DisableView();
            IsUpdateTike = false;
        }
    }
}
