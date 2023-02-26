using Resourse;
using UnityEngine;

namespace Building
{
    public class Creatorlog : Creator
    {
        public WarehouseCreateResourse<Log, Board> WarehouseResourse;
        public float TimeCreateOneProduct;
        public float TimeCreateEndProduct;
        public bool isCreateProduct { get; set; } = false;

        private void FixedUpdate()
        {
            if (WarehouseResourse != null && WarehouseResourse.InputResources[0] != null
                && WarehouseResourse.ExitingResourse.Count < WarehouseResourse.CapacityExitR) isCreateProduct = true;
            else isCreateProduct = false;
        }

        private void Update()
        {
            if (isCreateProduct == false) return;

            TimeCreateOneProduct += Time.deltaTime;
            if (TimeCreateEndProduct <= TimeCreateOneProduct)
            {
                TimeCreateOneProduct = 0;
                Test(WarehouseResourse.InputResources[0]);
            }
        }

        public void Test(Log log)
        {
            WarehouseResourse.InputResources.Remove(log);
            WarehouseResourse.ExitingResourse.Add(Create<Log, Board>(log));
        }
    }
}