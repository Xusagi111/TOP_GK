using Resource;
using System.Collections.Generic;
using UnityEngine;

namespace Building
{
    //Рассположение основных точек взаимодействия на префабе 
    public class DataBulding : MonoBehaviour 
    {
        [field: SerializeField] public ContactWithTheObject ConstructionBulding { get; private set; }
        [field: SerializeField] public ContactWithTheObject GetPointResource { get; private set; }
        [field: SerializeField] public ContactWithTheObject AddResourse { get; private set; }
        [field: SerializeField] public GameObject FinalView { get; private set; } //Финальный вид здания, изначально он будет не построен.
        [field: SerializeField] public float TimeCreatingSingleUnit { get; private set; }

        [field: SerializeField] public WarehouseResourcesForBuildingConstruction WarhouseConstruct  { get; private set; }

        public void Init()
        {
            ConstructionBulding.gameObject.SetActive(true);
            GetPointResource.gameObject.SetActive(false);
            AddResourse.gameObject.SetActive(false);
            WarhouseConstruct = this.gameObject.AddComponent<WarehouseResourcesForBuildingConstruction>();
        }

        public void EndCreatingFactory()
        {
            if (WarhouseConstruct != null) Destroy(WarhouseConstruct);

            ConstructionBulding.gameObject.SetActive(false);
            GetPointResource.gameObject.SetActive(true);
            AddResourse.gameObject.SetActive(true);
            var Creator = this.gameObject.AddComponent<CreateResource>();
            Creator.Init();

            ResourceWarhouse AddResource = new ResourceWarhouse(EnumResource.Log);
            ResourceWarhouse GetResource = new ResourceWarhouse(EnumResource.Board);

            Creator.AddResourceWarhouse.NewInit(AddResourse.transform, AddResource);
            Creator.GetResource.NewInit(GetPointResource.transform, GetResource);

            Creator._checkCreateResource = true;

       

            GetPointResource.EventToContact.AddListener(Creator.GetContactResource);
            AddResourse.EventToContact.AddListener(Creator.AddResource);
        }
        
        public void EndCreatingIcomeHouse()
        {
            ConstructionBulding.gameObject.SetActive(false);
            AddResourse.gameObject.SetActive(true);
            var Creator = this.gameObject.AddComponent<CreatorIcome>();
            const int TimeCreateOneProduct = 3;
            Creator.init(TimeCreateOneProduct, AddResourse.gameObject.transform);
            //Добавить компонент который будет производить деньги, или же другую продукцию. 
        }
    }
}