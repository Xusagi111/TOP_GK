using Assets.Script.Installer.App.Building;
using Assets.Script.Player.Interfaces;
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
        private LinkContact _linkCoroutineCreateR = new LinkContact(); 
        private LinkContact _linkCoroutineGetRes = new LinkContact(); 
        private LinkContact _linkCoroutineAddRes = new LinkContact();
        private UpdateTimeCreateR _updateTimeCreateR;
        private ConfigBuilding _configData;

        public StateBuildingCreateRes(DataBulding dataBulding, UpdateTimeCreateR updateTimeCreateR, ConfigBuilding ConfigData)
        {
            CurrentTypeRes = GetTypeBuilding.GetTypeRes(typeof(AddResource));
            CurrentGetTypeRes = GetTypeBuilding.GetTypeRes(typeof(GetResource));
            _addResTrigger = dataBulding.AddRes;
            _getResTrigger = dataBulding.GetRes;
            DataBulding = dataBulding;
            UI = dataBulding.UI;
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
                .Subscribe(collider => InventoryContact.AddCoCollizionRes(collider.gameObject, BaseWarehouse, _addResTrigger.transform, _linkCoroutineAddRes))
                .AddTo(Disposable);

            _getResTrigger.OnTriggerEnterAsObservable()
                 .Subscribe(collider => InventoryContact.GetCollizionRes(collider.gameObject, GetRes, _getResTrigger.transform, _linkCoroutineGetRes))
                 .AddTo(Disposable);

            _addResTrigger.OnTriggerExitAsObservable()
                .Subscribe(collider => InventoryContact.RemoveCollizionRes(collider.gameObject, _linkCoroutineAddRes))
                .AddTo(Disposable);

            _getResTrigger.OnTriggerExitAsObservable().
                Subscribe(collider => InventoryContact.RemoveCollizionRes(collider.gameObject, _linkCoroutineGetRes))
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

            if (_linkCoroutineCreateR.UpdateTimeCor == null &&
                BaseWarehouse.AllGameObj.Count > 1 &&
                GetRes.MaxElement > GetRes.AllGameObj.Count &&
                _updateTimeCreateR != null ) 
            {
                CreateRes();
            }
        }

        public void CreateRes()
        {
            _linkCoroutineCreateR.UpdateTimeCor = _updateTimeCreateR.UpdateTime(_linkCoroutineCreateR, BaseWarehouse, GetRes.AllGameObj, GetRes.TypeRes,
                _configData.TimeCreateOneRes, UI.ConstructT);
            Coroutines.instance.StartCoroutine(_linkCoroutineCreateR.UpdateTimeCor);
        }
    }
}

public class LinkContact
{
    public IEnumerator UpdateTimeCor;
    public Inventory UserInventory;
}

