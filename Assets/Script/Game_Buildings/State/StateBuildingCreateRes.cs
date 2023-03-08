using Assets.Script.Installer.App.Building;
using Building;
using Resource;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Assets.Script.Game_Buildings.State
{
    public class StateBuildingCreateRes<GetResource> : StateBaseBuilbing
    {
        protected EnumResource CurrentGetTypeRes;
        protected BaseWarehouse GetRes;
        private Collider _getResTrigger;

        private LinkContact _linkCoroutine = new LinkContact();
        private DataBulding _dataBulding;
        private UpdateTimeCreateR _updateTimeRes;
        private ConfigBuilding _configData;

        private LinkContact _linkCoroutineGetRes;

        public StateBuildingCreateRes(DataBulding dataBulding, UpdateTimeCreateR UpdateTimeRes, ConfigBuilding ConfigData)
        {
            CurrentGetTypeRes = GetTypeBuilding.GetTypeRes(typeof(GetResource));
            _getResTrigger = dataBulding.GetRes;
            _updateTimeRes = UpdateTimeRes;
            _dataBulding = dataBulding;
            _configData = ConfigData;
            UI = dataBulding.UI;
        }

        public override void Enter()
        {
            _dataBulding.EndViewCreatingIcomeBuildings();
            IsUpdateTike = true;

            _getResTrigger.OnTriggerEnterAsObservable()
            .Subscribe(collider => InventoryContact.GetCollizionRes(collider.gameObject, GetRes.AllResorce, _getResTrigger.transform, _linkCoroutineGetRes))
            .AddTo(Disposable);

            _getResTrigger.OnTriggerExitAsObservable().
                Subscribe(collider => InventoryContact.RemoveCollizionRes(collider.gameObject, _linkCoroutineGetRes))
                .AddTo(Disposable);
        }

        public override void IUpdate()
        {
            if (IsUpdateTike == false) return;
            if (_linkCoroutine.UpdateTimeCor == null &&
                BaseWarehouse.AllGameObj.Count > 1 &&
                GetRes.AllResorce.MaxElement > GetRes.AllResorce.AllGameObj.Count)
            {
                CreateRes();
            }
        }

        public void CreateRes()
        {
            _linkCoroutine.UpdateTimeCor = _updateTimeRes.UpdateTime(_linkCoroutine,BaseWarehouse, GetRes.AllResorce.AllGameObj, 
                BaseWarehouse.TypeRes, _configData.TimeCreateOneRes, UI.ConstructT);
            Coroutines.instance.StartCoroutine(_linkCoroutine.UpdateTimeCor);


        }

        public override void Exit()
        {
            DataBulding.DisableView();
            IsUpdateTike = false;
        }
    }
}
