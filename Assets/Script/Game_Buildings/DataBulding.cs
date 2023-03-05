using TMPro;
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

        [field: SerializeField] public WarehouseResourcesForBuildingConstruction WarhouseConstruct  { get; private set; } //Test Vizialization

        [field: SerializeField] public TextMeshProUGUI TimeCreateOneResource { get; private set; }

        public void Init()
        {
            ConstructionBulding.gameObject.SetActive(true);
            GetPointResource.gameObject.SetActive(false);
            AddResourse.gameObject.SetActive(false);
            WarhouseConstruct = this.gameObject.AddComponent<WarehouseResourcesForBuildingConstruction>();
            WarhouseConstruct.Init(TimeCreateOneResource);
        }

        public void EndCreatingFactory() 
        {
            if (WarhouseConstruct != null) Destroy(WarhouseConstruct);

            ConstructionBulding.gameObject.SetActive(false);
            GetPointResource.gameObject.SetActive(true);
            AddResourse.gameObject.SetActive(true);
        }
        
        public void EndCreatingIcomeHouse()
        {
            if (WarhouseConstruct != null) Destroy(WarhouseConstruct);

            ConstructionBulding.gameObject.SetActive(false);
            GetPointResource.gameObject.SetActive(true);
        }
    }
}