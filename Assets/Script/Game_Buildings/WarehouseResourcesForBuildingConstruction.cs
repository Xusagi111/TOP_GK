using Assets.Script.Player;
using Resourse;
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
            var PLayerInventory = CheckingInventory.GetComponent<TestPlayerInventory>();
            if (PLayerInventory == null)
            {
                Debug.LogError("Ошибка у игрока не найден инвентарь");
                return;
            }

            var Inventory = PLayerInventory.AllResoursePlayer;

            if (Inventory != null)
            {
                if (LogResource != null && LogicContact != null) LogicContact.StartCoroutine(
                    LogicContact.GetResourceInventoryToCreateProduct<Log>(LogResource, Inventory, EndMovePositionResource));

                if (BoardResource != null && LogicContact != null) LogicContact.StartCoroutine(
                    LogicContact.GetResourceInventoryToCreateProduct<Board>(BoardResource, Inventory, EndMovePositionResource));
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