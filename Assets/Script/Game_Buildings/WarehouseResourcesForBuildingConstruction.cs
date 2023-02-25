using Resourse;
using System.Collections.Generic;
using UnityEngine;

namespace Building
{
    public class WarehouseResourcesForBuildingConstruction : BaseWarehouse
    {
        private LogicContact _logicContact;
        private Transform _endMovePositionResource;
        private bool _isinit { get; set; } = false;

        public void Init(Transform EndMovePositionResource, ResourceWarhouse<Log> LogRes = null, ResourceWarhouse<Board> BoardRes = null)
        {
            if (_isinit == true) return;
            LogResource = LogRes;
            BoardResource = BoardRes;
            _logicContact = this.gameObject.AddComponent<LogicContact>();
            _endMovePositionResource = EndMovePositionResource;
        }

        public void AddResource(GameObject CheckingInventory)
        {
            //TODO Осуществить проверку на инвентарь с ресурсами, если это не будет сделанно, то он не пройдёт дальше.
            var BaseResourse = CheckingInventory.GetComponent<BaseResourse>();
            List<BaseResourse> Inventory = new List<BaseResourse>();
            Inventory.Add(BaseResourse); 
            
            if (BaseResourse != null)
            {
                _logicContact?.StartCoroutine(_logicContact.GetResource(this, Inventory, _endMovePositionResource));
            }
        }

    }


    [System.Serializable]
    public class ResourceWarhouse<T> 
    {
        public int CountElement;
        public int MaxElement;
    }
}