using Assets.Script.Player;
using Resource;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Building
{
    public class WarehouseResourcesForBuildingConstruction : MonoBehaviour
    {
        [field: SerializeField] public UnityEvent EventFullingResource { get; set; } = new UnityEvent();
        private bool _isFullingRes { get; set; } = false;
        [field: SerializeField] public TextMeshProUGUI TimeCreateOneResourceT { get; private set; }
        private bool _isInit { get; set; } = false;
        private BaseWarehouse _allBaseRes;

        public  void Init(TextMeshProUGUI TimeCreateOneResource)
        {
            this.TimeCreateOneResourceT = TimeCreateOneResource;
            _isInit = true;
        }

        public void AddResource(GameObject CheckingInventory)
        {
            Debug.Log("Contact");
            var PLayerInventory = CheckingInventory.GetComponent<TestPlayerInventory>();
            if (PLayerInventory == null || PLayerInventory?.AllResoursePlayer == null)
            {
                Debug.LogError("Ошибка у игрока не найден инвентарь");
                return;
            }

            var Inventory = PLayerInventory.AllResoursePlayer;
            var item = _allBaseRes.AllResorce;
            _allBaseRes.InventoryContact.StartCoroutine(_allBaseRes.InventoryContact.GetResourceInventoryToCreateProduct(item, Inventory, _allBaseRes.EndMovePositionResource));
        }

        private void FixedUpdate()
        {
            if (_isInit == false) return;

            if (_isFullingRes == false && CheckFullingResource()) 
            {
                Debug.Log("Завершение строительства");
                _isFullingRes = true;
                EventFullingResource?.Invoke();
            }

            TimeCreateOneResourceT.text = $"{_allBaseRes.AllResorce.TypeRes} {_allBaseRes.AllResorce.MaxElement - _allBaseRes.AllResorce.AllGameObj.Count}";
        }

        private void OnDestroy()
        {
            Destroy(_allBaseRes.InventoryContact);
            foreach (var item in _allBaseRes.AllResorce.AllGameObj) Destroy(item.gameObject);
            EventFullingResource.RemoveAllListeners();
        }

        private bool CheckFullingResource()
        {
            if (_allBaseRes.AllResorce.AllGameObj.Count == _allBaseRes.AllResorce.MaxElement) return true;
            else return false;
        }
    }

    [System.Serializable]
    public class ResourceWarhouse 
    {
        public int MaxElement = 10;
        public List<BaseResource> AllGameObj = new List<BaseResource>();
        public EnumResource TypeRes { get; private set; }
        public InventoryContact InventoryContact;
        public Transform EndMovePositionResource;

        public ResourceWarhouse(EnumResource TypeRes, InventoryContact InventoryContact, Transform EndMovePositionResource)
        {
            this.TypeRes = TypeRes;
            this.InventoryContact = InventoryContact;
            this.EndMovePositionResource = EndMovePositionResource;
        }
    }
}