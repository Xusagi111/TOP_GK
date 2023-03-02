using Resource;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Building
{
    //При добавление новых ресурсов, класс должен расшириться.

    public class LogicContact : MonoBehaviour
    {
        public IEnumerator GetResourceInventoryToCreateProduct<T>(ResourceWarhouse<T> Warehouse, List<BaseResource> Inventory, Transform EndMovePosition) where T : BaseResource
        {
            List<BaseResource> AllResource = new List<BaseResource>();
            var CountAllElement = Warehouse.CountElement;

            foreach (var item in Inventory)
            {
                if (CountAllElement < Warehouse.MaxElement)
                {
                    Debug.LogError(item.GetType() + "   " + typeof(T));
                    if (item.GetType() == typeof(T))
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


        public IEnumerator GetAllResource<T>(ResourceWarhouse<T> Warehouse, List<BaseResource> Inventory, Transform EndMovePosition) where T : BaseResource
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


        public float MoveAnimationObj<T>(List<BaseResource> Resource, ResourceWarhouse<T> EndINventory, Transform EndPosition, List<BaseResource> Inventory) where T : BaseResource
        {
            if (EndINventory == null)
            {
                Debug.LogError("Inventory Null");
                return 0;
            }
            if (EndINventory.MaxElement < EndINventory.CountElement + 1)
            {
                return 0;
            }
            var DelResource = Resource[0];
            DelResource.transform.position = EndPosition.position;
            EndINventory.CountElement++;
            Inventory.Remove(DelResource);
            
            Resource.Remove(DelResource);
            Destroy(DelResource.gameObject);
            return Global.s_TimeMoveResourse;
        }

        public float MoveAnimationToPlayer<T>(List<BaseResource> Resourse, ResourceWarhouse<T> EndINventory, Transform EndPosition, List<BaseResource> Inventory) where T : BaseResource
        {   
            if (EndINventory == null || EndINventory?.AllGameObj == null)
            {
                Debug.LogError("Inventory Null");
                return 0;
            }
            if (EndINventory.CountElement == 0)
            {
                return 0;
            }

            Resourse.Add(EndINventory.AllGameObj[0]);
            EndINventory.AllGameObj.RemoveAt(0);
            EndINventory.CountElement--;
            Resourse[0].transform.position = EndPosition.position;
            return Global.s_TimeMoveResourse;
        }
    }
}