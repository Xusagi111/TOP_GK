using Assets.Script.Player;
using Assets.Script.Player.Interfaces;
using UnityEngine;

namespace Building
{
    public static class GetInventory 
    {
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