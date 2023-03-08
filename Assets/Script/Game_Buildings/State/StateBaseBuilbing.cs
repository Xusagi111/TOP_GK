using Assets.Script.Player.Interfaces;
using Building;
using Resource;
using UniRx;
using UnityEngine;

namespace Assets.Script.Game_Buildings.State
{
    public abstract class StateBaseBuilbing : IBuildingState
    {
        [field: SerializeField] public ResourceWarhouse BaseWarehouse;
        [field: SerializeField] public DataBulding DataBulding;
        protected CompositeDisposable Disposable = new CompositeDisposable();
        protected Inventory IInventoryPlayer;
        protected EnumResource CurrentTypeRes = EnumResource.NullType;
        protected bool IsUpdateTike = false;

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void IUpdate() { }
    }
}
