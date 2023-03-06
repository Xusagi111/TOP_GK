using Assets.Script.Player.Interfaces;
using Resource;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Building
{
    public class LogicContact : MonoBehaviour
    {
        //При добавлении модифициаруем эту область.
        public IEnumerator GetResourceInventoryToCreateProduct(ResourceWarhouse Warehouse, List<BaseResource> Inventory, Transform EndMovePosition)
        {
            bool isLogWar = Warehouse.TypeRes == EnumResource.Log;
            bool isBoardWar = Warehouse.TypeRes == EnumResource.Board;
            Debug.LogError("Warhouse is Log: " + isLogWar + "   Warhouse is Board: " + isBoardWar);

            List <BaseResource> AllResource = new List<BaseResource>();
            var CountAllElement = Warehouse.CountElement;

            foreach (var item in Inventory)
            {
                if (CountAllElement < Warehouse.MaxElement)
                {
                    if (isLogWar && item.TypeRes == EnumResource.Log ||
                       isBoardWar && item.TypeRes == EnumResource.Board)
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

            if (Warehouse.CountElement != 0)
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
            if (EndINventory.MaxElement < EndINventory.CountElement + 1) return 0;

            var DelResource = Resource[0];
            DelResource.transform.position = EndPosition.position;
            EndINventory.CountElement = EndINventory.AllGameObj.Count;

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
            EndINventory.CountElement = EndINventory.AllGameObj.Count;

            return Global.s_TimeMoveResourse;
        }
    }
}