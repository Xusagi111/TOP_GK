using Building;
using UniRx.Triggers;
using UniRx;

namespace Assets.Script.Game_Buildings.State
{
    public class ConstructionBuilding<AddRes, GetRes> : StateBaseBuilbing
    {
        private BaseBuildingsState<AddRes, GetRes> _baseBuildingsState;
        private LinkContact _linkCoroutine = new LinkContact();

        public ConstructionBuilding(DataBulding dataBulding, BaseBuildingsState<AddRes, GetRes> baseBuildingsState)
        {
            DataBulding = dataBulding;
            this._baseBuildingsState = baseBuildingsState;
            BaseWarehouse = new ResourceWarhouse(GetTypeBuilding.GetTypeRes(typeof(AddRes)), DataBulding.ConstructionBulding.transform);
            UI = dataBulding.UI;
        }

        public override void Enter()
        {
            DataBulding.ConstructViewBuilding();
            IsUpdateTike = true;

            //Добиться того, чтобы возвращались рессурсы.
            DataBulding.ConstructionBulding.OnTriggerEnterAsObservable()
               .Subscribe(collider =>
               InventoryContact.AddCoCollizionRes(collider.gameObject, BaseWarehouse, DataBulding.ConstructionBulding.transform, _linkCoroutine))
               .AddTo(Disposable);

            DataBulding.ConstructionBulding.OnTriggerExitAsObservable()
          .Subscribe(collider => InventoryContact.RemoveCollizionRes(collider.gameObject, _linkCoroutine))
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
            //TODO Прикрепить изображение ресурсов
            UI.ConstructT.text = $"{BaseWarehouse.AllGameObj.Count} / {BaseWarehouse.MaxElement} + {typeof(AddRes)}";  

            if (BaseWarehouse.AllGameObj.Count >= BaseWarehouse.MaxElement)
            {
                UI.ConstructT.text = "";
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
