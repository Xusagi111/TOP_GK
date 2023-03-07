using Assets.Script.Player;
using Assets.Script.Player.Interfaces;
using Resource;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Building
{
    public class InventoryContact
    {
        public IEnumerator GetResourceInventoryToCreateProduct(ResourceWarhouse Warehouse, List<BaseResource> Inventory, Transform EndMovePosition)
        {
            List <BaseResource> AllResource = new List<BaseResource>();
            var CountAllElement = Warehouse.AllGameObj.Count;

            foreach (var item in Inventory)
            {
                if (CountAllElement < Warehouse.MaxElement)
                {
                    if (item.TypeRes == Warehouse.TypeRes)
                    {
                        CountAllElement++;
                        AllResource.Add(item);
                    }
                }
                else break;
            }

            int CountAllResourceInt = AllResource.Count;
            for (int i = 0; i < CountAllResourceInt; i++)
            {
               yield return MoveAnimationObj(AllResource, Warehouse, EndMovePosition, Inventory);
            }
        }

        public IEnumerator GetAllResource(ResourceWarhouse Warehouse, Inventory Inventory, Transform EndMovePosition)
        {
            List<BaseResource> AllResource = new List<BaseResource>();

            if (Warehouse.AllGameObj.Count != 0)
            {
                foreach (var item in Warehouse.AllGameObj) AllResource.Add(item);
            }

            int CountAllResourceInt = AllResource.Count;
            for (int i = 0; i < CountAllResourceInt; i++)
            {
                yield return MoveAnimationObj(AllResource, Warehouse, EndMovePosition, Inventory);
            }
        }


        public float MoveAnimationObj(List<BaseResource> Resource, ResourceWarhouse EndINventory, Transform EndPosition, List<BaseResource> Inventory)
        {
            if (EndINventory == null) { Debug.LogError("Inventory Null"); return 0; }
            if (EndINventory.MaxElement < EndINventory.AllGameObj.Count + 1) return 0;

            var DelResource = Resource[0];
            DelResource.transform.position = EndPosition.position;

            Inventory.Remove(DelResource);
            Resource.Remove(DelResource);

            EndINventory.AllGameObj.Add(DelResource);
            
            return Global.s_TimeMoveResourse;
        }

        public float MoveAnimationObj(List<BaseResource> Resource, ResourceWarhouse EndINventory, Transform EndPosition, Inventory Inventory)
        {
            if (EndINventory == null) { Debug.LogError("Inventory Null"); return 0; }
            if (Inventory.MaxCountElement < Inventory.AllResoursePlayer.Count + 1) return 0;

            var DelResource = Resource[0];
            DelResource.transform.position = EndPosition.position;


            Inventory.AllResoursePlayer.Add(DelResource);
            Resource.Remove(DelResource);
            EndINventory.AllGameObj.Remove(DelResource);

            return Global.s_TimeMoveResourse;
        }

        public static Inventory GetInventoryUser(GameObject CheckingInventory)
        {
            var PLayerInventory = CheckingInventory.GetComponent<TestPlayerInventory>();
            if (PLayerInventory == null) return null;
            else return PLayerInventory;
        }

        public static bool CheckingNullPlayerINventory(Inventory inventory)
        {
            if (inventory == null ||
                inventory.AllResoursePlayer == null ||
                inventory.AllResoursePlayer.Count == 0) { Debug.LogError("Ошибка у игрока не найден инвентарь"); return true; }
            else return false;
        }
    }
}