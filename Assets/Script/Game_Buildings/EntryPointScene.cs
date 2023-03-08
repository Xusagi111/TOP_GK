using Assets.Script.Game_Buildings.State.NewState;
using Assets.Script.Player;
using Assets.Script.Player.Interfaces;
using Resource;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Building
{
    public class EntryPointScene : MonoBehaviour
    {
        [Inject]
        private List<BaseResource> _allTypeRes;
        [Inject]
        private BuildingsStateLog _oneHousePrefab;

        private void Start()
        {
            CreatePlayer();
            var CreateHouse = Instantiate(_oneHousePrefab, Vector3.zero, Quaternion.identity);
            CreateHouse.SetConstruct();
        }

        private void CreatePlayer()
        {
            GameObject TestPlayer = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            TestPlayer.transform.position = new Vector3(10,0.5f);
            TestPlayer.name = "Test_PLayer";
            TestPlayer.AddComponent<Rigidbody>();
            var InvenoryPlayer = TestPlayer.AddComponent<TestPlayerInventory>();
            CreatePlayerRes(InvenoryPlayer);
        }

        private void CreatePlayerRes(Inventory InvenoryPlayer)
        {
            BaseResource PrefabLogRes = null;
            BaseResource PrefabBoardRes = null;
            foreach (var item in _allTypeRes)
            {
                if (item.TypeRes == EnumResource.Log) PrefabLogRes = item;
                if (item.TypeRes == EnumResource.Board) PrefabBoardRes = item;
            }

            for (int i = 0; i < 30; i++)
            {
                var BaseRes = Instantiate(PrefabLogRes, new Vector3(15, 0.5f, 1f), Quaternion.identity);
                BaseRes.name = "Log";
                InvenoryPlayer.AllResoursePlayer.Add(BaseRes);
            }

            for (int i = 0; i < 5; i++)
            {
                var BaseRes = Instantiate(PrefabBoardRes, new Vector3(15, 0.5f, 1f), Quaternion.identity);
                BaseRes.name = "Board";
                InvenoryPlayer.AllResoursePlayer.Add(BaseRes);
            }
        }
    }
}