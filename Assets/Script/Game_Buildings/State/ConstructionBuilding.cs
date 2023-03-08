using Building;
using UniRx.Triggers;
using UniRx;

namespace Assets.Script.Game_Buildings.State
{
    public class ConstructionBuilding<AddRes, GetRes> : StateBaseBuilbing
    {
        private BaseBuildingsState<AddRes, GetRes> _baseBuildingsState;

        public ConstructionBuilding(DataBulding dataBulding, BaseBuildingsState<AddRes, GetRes> baseBuildingsState)
        {
            DataBulding = dataBulding;
            this._baseBuildingsState = baseBuildingsState;
            BaseWarehouse = new ResourceWarhouse(GetTypeBuilding.GetTypeRes(typeof(AddRes)), DataBulding.ConstructionBulding.transform);
        }

        public override void Enter()
        {
            DataBulding.ConstructViewBuilding();
            IsUpdateTike = true;

            DataBulding.ConstructionBulding.OnTriggerEnterAsObservable()
               .Subscribe(collider => InventoryContact.AddCoCollizionRes(collider.gameObject, BaseWarehouse, DataBulding.ConstructionBulding.transform))
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
            if (BaseWarehouse.AllGameObj.Count >= BaseWarehouse.MaxElement)
            {
                IsUpdateTike = false;
                switch (DataBulding.SwitchingStateConst)
                {
                    case StateEnem.CreateOneRes:
                        _baseBuildingsState.SetCreateResNoAddResource();
                        break;
                    case StateEnem.GetAndCreateRes:
                        _baseBuildingsState.SetCreateRes();
                        break;
                }
            }
        }
    }
}
