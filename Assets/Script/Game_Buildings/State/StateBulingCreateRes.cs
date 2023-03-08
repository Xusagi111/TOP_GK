using Building;
using Resource;
using System.Collections;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Assets.Script.Game_Buildings.State
{
    [System.Serializable]
    public class StateBuildingCreateRes<AddResource, GetResource> : StateBaseBuilbing
    {
        protected EnumResource CurrentGetTypeRes;
        private Collider _addResTrigger;
        private Collider _getResTrigger;
        private float TimeCreateOneElement;
        protected BaseWarehouse GetRes;
        private IEnumerator _updateTimeCreateR;

        public StateBuildingCreateRes(DataBulding dataBulding)
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

            _addResTrigger.OnTriggerEnterAsObservable()
               .Subscribe(collider => InventoryContact.AddCoCollizionRes(collider.gameObject, BaseWarehouse, _addResTrigger.transform))
               .AddTo(Disposable);

            _getResTrigger.OnTriggerEnterAsObservable()
            .Subscribe(collider => InventoryContact.GetCollizionRes(collider.gameObject, GetRes.AllResorce, _getResTrigger.transform))
            .AddTo(Disposable);
        }

        public override void Exit()
        {
            DataBulding.DisableView();
            IsUpdateTike = false;
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
            _updateTimeCreateR = UpdateTimeCreateR.UpdateTime(BaseWarehouse, GetRes.AllResorce.AllGameObj, BaseWarehouse.TypeRes,
                TimeCreateOneElement, DataBulding.TimeCreateOneResourceT);
            Coroutines.instance.StartCoroutine(_updateTimeCreateR);
        }
    }
}
