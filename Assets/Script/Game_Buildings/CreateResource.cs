using Assets.Script.Player;
using Assets.Script.Player.Interfaces;
using Building;
using Resource;
using UnityEngine;

namespace Building
{
    public class CreateResource : MonoBehaviour
    {
        [field: SerializeField] public TestCreateResource AddResourceWarhouse { get; protected set; }
        [field: SerializeField] public TestCreateResource GetResource { get; protected set; }
        public bool _checkCreateResource { get; set; } = false;

        public void Init()
        {
            AddResourceWarhouse = this.gameObject.AddComponent<TestCreateResource>();
            GetResource = this.gameObject.AddComponent<TestCreateResource>();
        }

        private void FixedUpdate()
        {
            if (_checkCreateResource == false) return;

            var item = AddResourceWarhouse.AllResorce;
            if (item.CountCreateResource <= item.CountElement &&
                GetResource.AllResorce.MaxElement > GetResource.AllResorce.CountElement)
            {
                CreateResourceM(AddResourceWarhouse, GetResource);
            }
        }

        public void AddResource(GameObject CheckingInventory)
        {
            var PLayerInventory = GetInventoryUser(CheckingInventory);
            if (CheckingNullPlayerINventory(PLayerInventory)) return;

            var Inventory = PLayerInventory.AllResoursePlayer;
            var item = AddResourceWarhouse.AllResorce;
            var WarhouseR = AddResourceWarhouse.AllResorce;
            if (WarhouseR.AllGameObj.Count < WarhouseR.MaxElement)
            {
                StartCoroutine(AddResourceWarhouse.LogicContact.GetResourceInventoryToCreateProduct(item, Inventory, AddResourceWarhouse.EndMovePositionResource));
            }
        }

        public void GetContactResource(GameObject CheckingInventory)
        {
            Debug.LogWarning("GetContactResource");
            var Inventory = GetInventoryUser(CheckingInventory);
            if (CheckingNullPlayerINventory(Inventory)) return;

            var item = GetResource.AllResorce;
            if (item.AllGameObj != null && Inventory.AllResoursePlayer.Count < Inventory.MaxCountElement)
            {
                StartCoroutine(GetResource.LogicContact.GetAllResource(item, Inventory, CheckingInventory.gameObject.transform));
            }
        }

        private void CreateResourceM(BaseWarehouse WarhoureRes, BaseWarehouse EndWarhouseRes)
        {
            var AllBaseResource = Buildings.instance.AllInstanceResource;
            EnumResource TypeCreateRes = EndWarhouseRes.AllResorce.TypeRes;
            BaseResource PrefabCreateRes = null;

            foreach (var item in AllBaseResource) 
            {
                if (item.TypeRes == TypeCreateRes)
                {
                    PrefabCreateRes = item;
                    break;
                }
            }

            var ListGetRes = WarhoureRes.AllResorce.AllGameObj;
            var ListSetRes = EndWarhouseRes.AllResorce.AllGameObj;

            if (PrefabCreateRes != null && ListGetRes.Count > 0)
            {
                var CreateR = Instantiate(PrefabCreateRes, Vector3.one, Quaternion.identity);
                CreateR.transform.SetParent(EndWarhouseRes.EndMovePositionResource);
                CreateR.transform.position = Vector3.one;
                var CurrentGetRes = ListGetRes[0];
                WarhoureRes.AllResorce.AllGameObj.Remove(CurrentGetRes);
                Destroy(CurrentGetRes.gameObject);
                ListSetRes.Add(CreateR);
                EndWarhouseRes.AllResorce.CountElement = ListSetRes.Count;
            }
            else Debug.LogError("Данный рессурс равен null");
        }

        private Inventory GetInventoryUser(GameObject CheckingInventory) 
        {
            var PLayerInventory = CheckingInventory.GetComponent<TestPlayerInventory>();
            if (PLayerInventory == null) return null;
            else return PLayerInventory;
        }

        private bool CheckingNullPlayerINventory(Inventory inventory)
        {
            if (inventory == null ||
                inventory.AllResoursePlayer == null ||
                inventory.AllResoursePlayer.Count == 0) { Debug.LogError("Ошибка у игрока не найден инвентарь"); return true; }
            else return false;
        }
    }
}
