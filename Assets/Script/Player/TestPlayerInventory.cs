using Assets.Script.Player.Interfaces;
using Resource;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.Player
{
    public class TestPlayerInventory : MonoBehaviour, Inventory
    {
        [field: SerializeField]  public List<BaseResource> AllResoursePlayer { get; set; } = new List<BaseResource>();
        public int MaxCountElement { get; private set; } = 35;
        public Transform Position { get; set; }

        public void AddCellINventory(int AddCount) => MaxCountElement += AddCount;
        public void RemoveCellInventory(int RemoveCount) => MaxCountElement -= RemoveCount;

    }
}