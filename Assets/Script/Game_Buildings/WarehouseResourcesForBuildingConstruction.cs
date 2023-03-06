using Assets.Script.Player;
using Resource;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Building
{
    public class WarehouseResourcesForBuildingConstruction : BaseWarehouse
    {
        [field: SerializeField] public UnityEvent EventFullingResource { get; set; } = new UnityEvent();
        private bool _isFullingRes { get; set; } = false;
        [field: SerializeField] public TextMeshProUGUI TimeCreateOneResourceT { get; private set; }
        private bool _isInit { get; set; } = false;

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
            var item = AllResorce;
                LogicContact.StartCoroutine(LogicContact.GetResourceInventoryToCreateProduct(item, Inventory, EndMovePositionResource));
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

            TimeCreateOneResourceT.text = $"{AllResorce.TypeRes} {AllResorce.MaxElement - AllResorce.AllGameObj.Count}";
        }

        private void OnDestroy()
        {
            Destroy(LogicContact);
            foreach (var item in AllResorce.AllGameObj) Destroy(item.gameObject);
            EventFullingResource.RemoveAllListeners();
        }

        private bool CheckFullingResource()
        {
            if (AllResorce.AllGameObj.Count == AllResorce.MaxElement) return true;
            else return false;
        }

    }


    [System.Serializable]
    public class ResourceWarhouse 
    {
        public int CountElement;
        public int MaxElement = 10;
        public List<BaseResource> AllGameObj = new List<BaseResource>();
        public string NameRes { get; set; }
        public int CountCreateResource { get; set; } = 1; //Пока не используется. 
        public float TimeCreateOneResource { get; set; } = 3;
        public EnumResource TypeRes { get; private set; }

        public ResourceWarhouse(EnumResource TypeRes)
        {
           this.TypeRes = TypeRes;
        }
    }
}