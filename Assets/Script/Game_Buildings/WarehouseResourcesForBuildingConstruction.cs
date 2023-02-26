using Resourse;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Building
{
    public class WarehouseResourcesForBuildingConstruction : BaseWarehouse
    {
        [field: SerializeField] public UnityEvent EventFullingResource { get; set; } = new UnityEvent();
        private bool _isFullingRes { get; set; } = false;

        public void AddResource(GameObject CheckingInventory)
        {
            //TODO Осуществить проверку на инвентарь с ресурсами, если это не будет сделанно, то он не пройдёт дальше.
            var BaseResourse = CheckingInventory.GetComponent<BaseResource>();
            List<BaseResource> Inventory = new List<BaseResource>();
            Inventory.Add(BaseResourse); 
            
            if (BaseResourse != null)
            {
                LogicContact?.StartCoroutine(LogicContact.GetResourceInventoryToCreateProduct(this, Inventory, EndMovePositionResource));
            }
        }

        private void FixedUpdate()
        {
            if (_isFullingRes == false && 
                LogResource?.CountElement == LogResource?.MaxElement && 
                BoardResource?.CountElement == BoardResource?.MaxElement)
            {
                _isFullingRes = true;
                EventFullingResource?.Invoke();
            }
        }

        private void OnDestroy() => EventFullingResource.RemoveAllListeners();
    }


    [System.Serializable]
    public class ResourceWarhouse<T> 
    {
        public int CountElement;
        public int MaxElement;
    }
}