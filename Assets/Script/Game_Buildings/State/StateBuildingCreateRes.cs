using Assets.Script.Installer.App.Building;
using Building;
using Resource;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace Assets.Script.Game_Buildings.State
{
    [System.Serializable]
    public class StateBuildingCreateRes<GetResource> : StateBaseBuilbing
    {
        [Inject]
        private UpdateTimeCreateR _updateTimeRes;
        [Inject]
        private ConfigBuilding _configData;

        protected EnumResource CurrentGetTypeRes;
        protected BaseWarehouse GetRes;
        private Collider _getResTrigger;
        private LinkCoroutine _linkCoroutine = new LinkCoroutine();
        private DataBulding _dataBulding;

        public StateBuildingCreateRes(DataBulding dataBulding, UpdateTimeCreateR UpdateTimeRes, ConfigBuilding ConfigData)
        {
            CurrentGetTypeRes = GetTypeBuilding.GetTypeRes(typeof(GetResource));
            _getResTrigger = dataBulding.GetRes;
            _updateTimeRes = UpdateTimeRes;
            _dataBulding = dataBulding;
            _configData = ConfigData;
        }

        public override void Enter()
        {
            _dataBulding.EndViewCreatingIcomeBuildings();
            IsUpdateTike = true;
            _getResTrigger.OnTriggerEnterAsObservable()
            .Subscribe(collider => InventoryContact.GetCollizionRes(collider.gameObject, GetRes.AllResorce, _getResTrigger.transform))
            .AddTo(Disposable);
        }

        public override void IUpdate()
        {
            if (IsUpdateTike == false) return;


            if (_linkCoroutine.UpdateTimeCor == null &&
                BaseWarehouse.AllGameObj.Count > 1 &&
                GetRes.AllResorce.MaxElement > BaseWarehouse.AllGameObj.Count)
            {
                CreateRes();
            }
        }

        public void CreateRes()
        {
            _linkCoroutine.UpdateTimeCor = _updateTimeRes.UpdateTime(_linkCoroutine,BaseWarehouse, GetRes.AllResorce.AllGameObj, 
                BaseWarehouse.TypeRes, _configData.TimeCreateOneRes, DataBulding.TimeCreateOneResourceT);
            Coroutines.instance.StartCoroutine(_linkCoroutine.UpdateTimeCor);
        }

        public override void Exit()
        {
            DataBulding.DisableView();
            IsUpdateTike = false;
        }
    }
}
