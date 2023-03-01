using Resource;
using System.Collections.Generic;
using UnityEngine;

namespace Building
{
    //Рассположение основных точек взаимодействия на префабе 
    public class DataBulding : MonoBehaviour 
    {
        [field: SerializeField] public ContactWithTheObject ConstructionBulding { get; private set; }
        [field: SerializeField] public ContactWithTheObject СollectionPointResource { get; private set; }
        [field: SerializeField] public ContactWithTheObject GetCreateResourse { get; private set; }
        [field: SerializeField] public GameObject FinalView { get; private set; } //Финальный вид здания, изначально он будет не построен.
        [field: SerializeField] public float TimeCreatingSingleUnit { get; private set; }

        [field: SerializeField] public WarehouseResourcesForBuildingConstruction WarhouseConstruct  { get; private set; }

        public void Init()
        {
            ConstructionBulding.gameObject.SetActive(true);
            СollectionPointResource.gameObject.SetActive(false);
            GetCreateResourse.gameObject.SetActive(false);
            WarhouseConstruct = this.gameObject.AddComponent<WarehouseResourcesForBuildingConstruction>();
        }

        public void EndCreatingFactory()
        {
            if (WarhouseConstruct != null) Destroy(WarhouseConstruct);

            ConstructionBulding.gameObject.SetActive(false);
            СollectionPointResource.gameObject.SetActive(true);
            GetCreateResourse.gameObject.SetActive(true);
            var Creator = this.gameObject.AddComponent<CreateResource>();
            Creator.Init();

            Creator.AddResorseWArhouse.Init(GetCreateResourse.transform, new ResourceWarhouse<Log>() { MaxElement = 10 });
            Creator.GetResource.Init(GetCreateResourse.transform, null, new ResourceWarhouse<Board>() { MaxElement = 10, AllGameObj = new List<Board>() });

            СollectionPointResource.EventToContact.AddListener(Creator.AddResource);
            GetCreateResourse.EventToContact.AddListener(Creator.GetContactResource);
        }
        
        public void EndCreatingIcomeHouse()
        {
            ConstructionBulding.gameObject.SetActive(false);
            GetCreateResourse.gameObject.SetActive(true);
            var Creator = this.gameObject.AddComponent<CreatorIcome>();
            const int TimeCreateOneProduct = 3;
            Creator.init(TimeCreateOneProduct, GetCreateResourse.gameObject.transform);
            //Добавить компонент который будет производить деньги, или же другую продукцию. 
        }
    }
}