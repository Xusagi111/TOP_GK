using Resourse;
using UnityEngine;

namespace Building
{
    public class Creatorlog : BaseWarehouse
    {
        //public float TimeCreateOneProduct;
        //public float TimeCreateEndProduct;
        //public bool isCreateProduct { get; set; } = false;

        //private void FixedUpdate()
        //{
        //    if (WarehouseResourse != null && WarehouseResourse.InputResources[0] != null
        //        && WarehouseResourse.ExitingResourse.Count < WarehouseResourse.CapacityExitR) isCreateProduct = true;
        //    else isCreateProduct = false;
        //}

        //private void Update()
        //{
        //    if (isCreateProduct == false) return;

        //    TimeCreateOneProduct += Time.deltaTime;
        //    if (TimeCreateEndProduct <= TimeCreateOneProduct)
        //    {
        //        TimeCreateOneProduct = 0;
        //        Test(WarehouseResourse.InputResources[0]);
        //    }
        //}

        //public void Test(Log log)
        //{
        //    WarehouseResourse.InputResources.Remove(log);
        //    WarehouseResourse.ExitingResourse.Add(Create<Log, Board>(log));
        //}

        //public E Create<T, E>(T GetResourse) where E : Component
        //{
        //    var CreateObj = new GameObject();
        //    return CreateObj.AddComponent<E>();
        //}
    }
}