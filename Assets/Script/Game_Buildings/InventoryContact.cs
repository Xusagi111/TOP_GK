using Assets.Script.Player.Interfaces;
using Resource;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Building
{
    public class InventoryContact : MonoBehaviour
    {

        //При добавлении модифициаруем эту область.
        //public IEnumerator GetResourceInventoryToCreateProduct(ResourceWarhouse Warehouse, List<BaseResource> listRes, Transform EndMovePosition)
        //{

        //    List <BaseResource> AllResource = new List<BaseResource>();
            //var CountAllElement = Warehouse.CountElement;

            //foreach (var item in listRes)
            //{
            //    if (CountAllElement < Warehouse.MaxElement)
            //    {
            //        if (item.TypeRes == Warehouse.TypeRes)
            //        {
            //            CountAllElement++;
            //            AllResource.Add(item);
            //        }
            //    }
            //    else break;
            //}

            //int CountAllResourceInt = AllResource.Count;
            //for (int i = 0; i < CountAllResourceInt; i++)
            //{
            //   yield return MoveAnimationObj(AllResource, Warehouse, EndMovePosition, listRes);
            //}
        //}

        //public IEnumerator GetAllResource(ResourceWarhouse Warehouse, Inventory Inventory, Transform EndMovePosition)
        //{
        //    List<BaseResource> AllResource = new List<BaseResource>();

            //if (Warehouse.CountElement != 0)
            //{
            //    foreach (var item in Warehouse.AllGameObj) AllResource.Add(item);
            //}

            //int CountAllResourceInt = AllResource.Count;
            //for (int i = 0; i < CountAllResourceInt; i++)
            //{
            //    yield return MoveAnimationObj(AllResource, Warehouse, EndMovePosition, Inventory);
        //    //}
        //}

    }
}