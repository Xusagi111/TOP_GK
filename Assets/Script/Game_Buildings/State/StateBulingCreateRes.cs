using Building;
using Resource;
using System.Collections;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace Assets.Script.Game_Buildings.State
{
    [System.Serializable]
    public class StateBuildingCreateRes<AddResource, GetResource> : StateBaseBuilbing
    {
        [field:SerializeField] protected EnumResource CurrentGetTypeRes;
        private Collider _addResTrigger;
        private Collider _getResTrigger;
        private float TimeCreateOneElement;
        [field: SerializeField] public ResourceWarhouse GetRes { get; protected set; }
        private IEnumerator _updateTimeCreateRCor;

        private UpdateTimeCreateR _updateTimeCreateR;

        public StateBuildingCreateRes(DataBulding dataBulding, UpdateTimeCreateR updateTimeCreateR)
        {
            CurrentTypeRes = GetTypeBuilding.GetTypeRes(typeof(AddResource));
            CurrentGetTypeRes = GetTypeBuilding.GetTypeRes(typeof(GetResource));
            _addResTrigger = dataBulding.AddRes;
            _getResTrigger = dataBulding.GetRes;
            DataBulding = dataBulding;

            GetRes = new ResourceWarhouse(CurrentGetTypeRes, _getResTrigger.transform);
            BaseWarehouse = new ResourceWarhouse(CurrentTypeRes, _addResTrigger.transform);
            _updateTimeCreateR = updateTimeCreateR;
        }

        public override void Enter()
        {
            DataBulding.EndViewFactory();
            IsUpdateTike = true;

            _addResTrigger.OnTriggerEnterAsObservable()
               .Subscribe(collider => InventoryContact.AddCoCollizionRes(collider.gameObject, BaseWarehouse, _addResTrigger.transform))
               .AddTo(Disposable);

            _getResTrigger.OnTriggerEnterAsObservable()
            .Subscribe(collider => InventoryContact.GetCollizionRes(collider.gameObject, GetRes, _getResTrigger.transform))
            .AddTo(Disposable);
        }

        public override void Exit()
        {
            DataBulding.DisableView();
            IsUpdateTike = false;
        }

        public override void IUpdate()
        {
            if (IsUpdateTike == false) return;

            if (_updateTimeCreateRCor == null &&
                BaseWarehouse.AllGameObj.Count > 1 &&
                GetRes.MaxElement > BaseWarehouse.AllGameObj.Count &&
                _updateTimeCreateR != null) 
            {
                CreateRes();
            }
        }

        public void CreateRes()
        {
            _updateTimeCreateRCor = _updateTimeCreateR.UpdateTime(BaseWarehouse, GetRes.AllGameObj, BaseWarehouse.TypeRes,
                TimeCreateOneElement, DataBulding.TimeCreateOneResourceT);
            Coroutines.instance.StartCoroutine(_updateTimeCreateRCor);
        }
    }
}
