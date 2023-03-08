using Assets.Script;
using Assets.Script.Player;
using Assets.Script.Player.Interfaces;
using Resource;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Building
{
    public static class InventoryContact
    {
        public static IEnumerator GetResourceInventoryToCreateProduct(ResourceWarhouse Warehouse, Inventory inventory, Transform EndMovePosition)
        {
            var AllPlayerRes = inventory.AllResoursePlayer;
            List <BaseResource> AllResource = new List<BaseResource>();
            var CountAllElement = Warehouse.AllGameObj.Count;

            foreach (var item in AllPlayerRes)
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
                //Cделать метод который будет отключать корутину при удалении игрока от места триггера.
                float TimeUpdateFrame = MoveAnimationObj<Log>(AllResource, Warehouse, EndMovePosition, inventory);
                yield return new WaitForSeconds(TimeUpdateFrame);
            }
        }

        public static IEnumerator GetAllResource(ResourceWarhouse Warehouse, Inventory Inventory, Transform EndMovePosition)
        {
            List<BaseResource> AllResource = new List<BaseResource>();

            if (Warehouse.AllGameObj.Count != 0)
            {
                foreach (var item in Warehouse.AllGameObj) AllResource.Add(item);
            }

            int CountAllResourceInt = AllResource.Count;
            for (int i = 0; i < CountAllResourceInt; i++)
            {
                float TimeUpdateFrame = MoveAnimationObj(AllResource, Warehouse, EndMovePosition, Inventory);
                yield return new WaitForSeconds(TimeUpdateFrame);
            }
        }


        public static float MoveAnimationObj<T>(List<BaseResource> Resource, ResourceWarhouse EndINventory, Transform EndPosition, Inventory Inventory)
        {
            if (EndINventory == null) { Debug.LogError("Inventory Null"); return 0; }
            if (EndINventory.MaxElement < EndINventory.AllGameObj.Count + 1) return 0;

            var DelResource = Resource[0];
            DelResource.transform.position = EndPosition.position;

            Inventory.AllResoursePlayer.Remove(DelResource);
            Resource.Remove(DelResource);

            EndINventory.AllGameObj.Add(DelResource);
            
            return TestGlobal.s_TimeMoveResourse;
        }

        public static float MoveAnimationObj(List<BaseResource> Resource, ResourceWarhouse EndINventory, Transform EndPosition, Inventory Inventory)
        {
            if (EndINventory == null) { Debug.LogError("Inventory Null"); return 0; }
            if (Inventory.MaxCountElement < Inventory.AllResoursePlayer.Count + 1) return 0;

            var DelResource = Resource[0];
            DelResource.transform.position = EndPosition.position;


            Inventory.AllResoursePlayer.Add(DelResource);
            Resource.Remove(DelResource);
            EndINventory.AllGameObj.Remove(DelResource);

            return TestGlobal.s_TimeMoveResourse;
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

        public static void AddCoCollizionRes(GameObject Player, ResourceWarhouse baseWarehouse, Transform transform, LinkContact linkCoroutine)
        {
            Debug.Log("AddCoCollizionRes " + Player.name);
            var InventoryPlayer = InventoryContact.GetInventoryUser(Player);
            if (InventoryContact.CheckingNullPlayerINventory(InventoryPlayer)) return;

            var Cor = InventoryContact.GetResourceInventoryToCreateProduct(baseWarehouse, InventoryPlayer, transform);
            Coroutines.StartRoutine(Cor);
            linkCoroutine.UpdateTimeCor = Cor;
        }

        public static void GetCollizionRes(GameObject Player, ResourceWarhouse baseWarehouse, Transform transform, LinkContact linkCoroutine)
        {
            Debug.Log("GetCollizionRes " + Player.name);
            var InventoryPlayer = InventoryContact.GetInventoryUser(Player);
            if (InventoryContact.CheckingNullPlayerINventory(InventoryPlayer)) return;

            var Cor = InventoryContact.GetAllResource(baseWarehouse, InventoryPlayer, transform);
            Coroutines.StartRoutine(Cor);
            linkCoroutine.UpdateTimeCor = Cor;
        }

        public static void RemoveCollizionRes(GameObject Player, LinkContact linkCoroutine)
        {
            Debug.Log("RemoveCollizionRes " + Player.name);
            var InventoryPlayer = InventoryContact.GetInventoryUser(Player);
            if (InventoryContact.CheckingNullPlayerINventory(InventoryPlayer)) return;
            if (InventoryPlayer == linkCoroutine.UserInventory)
            {
                if (linkCoroutine.UpdateTimeCor != null) Coroutines.StopRoutine(linkCoroutine.UpdateTimeCor);
            }
        }
    }
}