using Assets.Script.Player.Interfaces;
using Building;
using Resource;
using UniRx;
using UnityEngine;
using Zenject;

namespace Assets.Script.Game_Buildings.State
{
    [System.Serializable]
    public abstract class StateBaseBuilbing : IBuildingState
    {
        protected EnumResource CurrentTypeRes = EnumResource.NullType;
        [field: SerializeField] public ResourceWarhouse BaseWarehouse;
        protected bool IsUpdateTike = false;
        [field: SerializeField] public DataBulding DataBulding;
        protected CompositeDisposable Disposable = new CompositeDisposable();
        protected Inventory IInventoryPlayer;

        public virtual void Enter()
        {
            //Производить инициализацию базового типа
        }

        public virtual void Exit()
        {
            //Дестроить тип
        }

        public virtual void IUpdate()
        {
            //Обновления
        }
    }
}
