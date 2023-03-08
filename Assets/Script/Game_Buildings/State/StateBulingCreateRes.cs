using Assets.Script.Installer.App.Building;
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
        [field:SerializeField] protected EnumResource CurrentGetTypeRes;
        private Collider _addResTrigger;
        private Collider _getResTrigger;
        [field: SerializeField] public ResourceWarhouse GetRes { get; protected set; }
        private LinkCoroutine _linkCoroutine = new LinkCoroutine(); 
        private UpdateTimeCreateR _updateTimeCreateR;
        private ConfigBuilding _configData;

        public StateBuildingCreateRes(DataBulding dataBulding, UpdateTimeCreateR updateTimeCreateR, ConfigBuilding ConfigData)
        {
            CurrentTypeRes = GetTypeBuilding.GetTypeRes(typeof(AddResource));
            CurrentGetTypeRes = GetTypeBuilding.GetTypeRes(typeof(GetResource));
            _addResTrigger = dataBulding.AddRes;
            _getResTrigger = dataBulding.GetRes;
            DataBulding = dataBulding;

            GetRes = new ResourceWarhouse(CurrentGetTypeRes, _getResTrigger.transform);
            BaseWarehouse = new ResourceWarhouse(CurrentTypeRes, _addResTrigger.transform);
            _updateTimeCreateR = updateTimeCreateR;
            _configData = ConfigData;
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

            if (_linkCoroutine.UpdateTimeCor == null &&
                BaseWarehouse.AllGameObj.Count > 1 &&
                GetRes.MaxElement > BaseWarehouse.AllGameObj.Count &&
                _updateTimeCreateR != null ) 
            {
                CreateRes();
            }
        }

        public void CreateRes()
        {
            _linkCoroutine.UpdateTimeCor = _updateTimeCreateR.UpdateTime(_linkCoroutine, BaseWarehouse, GetRes.AllGameObj, BaseWarehouse.TypeRes,
                _configData.TimeCreateOneRes, DataBulding.TimeCreateOneResourceT);
            Coroutines.instance.StartCoroutine(_linkCoroutine.UpdateTimeCor);
        }
    }
}

public class LinkCoroutine
{
    public IEnumerator UpdateTimeCor;
}

