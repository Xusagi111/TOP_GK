using Building;
using System.Collections;

namespace Assets.Script.Game_Buildings.State
{
    public class ConstructionBuilding<T> : StateBuilbing
    {
        private DataBulding _dataBulding;


        //Убрать от сюда new InventoryContact()
        public ConstructionBuilding(DataBulding dataBulding)
        {
            _dataBulding = dataBulding;
      
            CurrentTypeRes = GetTypeBuilding.GetTypeRes(typeof(T));
            BaseWarehouse = new ResourceWarhouse(CurrentTypeRes, new InventoryContact(), _dataBulding.ConstructionBulding.transform);
        }

        public override void Enter()
        {
            _dataBulding.ConstructViewBuilding();
            IsUpdateTike = true;
            //подписать на событие получения рес   _dataBulding.ConstructionBulding. 
        }

        public override void Exit()
        {
            _dataBulding.DisableView();
            IsUpdateTike = false;
            //отписать от события  _dataBulding.ConstructionBulding. 
        }
        public override void FixedTick()
        {
            if (IsUpdateTike == false) return;
            if (BaseWarehouse.AllGameObj.Count >= BaseWarehouse.MaxElement)
            {
                //Cмена состояния, сделать через UNIRX
            }
        }
    }
}
