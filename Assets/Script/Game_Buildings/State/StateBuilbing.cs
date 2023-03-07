using Building;
using Resource;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Script.Game_Buildings.State
{
    public abstract class StateBuilbing : IBuildingState, ITickable
    {
        protected EnumResource CurrentTypeRes = EnumResource.NullType;
        [field: SerializeField] public TextMeshProUGUI TimeCreateOneResourceT { get; private set; } //Заинжектить данный тип 
        protected bool IsUpdateTike = false;
        protected ResourceWarhouse BaseWarehouse;
        protected DataBulding DataBulding;

        public virtual void Enter()
        {
            //Производить инициализацию базового типа
        }

        public virtual void Exit()
        {
            //Дестроить тип
        }

        public virtual void Tick()
        {
            //Обновления
        }
    }
}
