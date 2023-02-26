using Resourse;
using UnityEngine;

namespace Building
{
    public class TestCreateHouse : MonoBehaviour
    {
        [field:SerializeField] private Transform _onePointCreateBuilding { get; set; }

        public void Start()
        {
            //Создание здания
            var House1 = Buildings.instance.House1;
            var NewHouse = Instantiate(House1, _onePointCreateBuilding.position, Quaternion.identity);
            NewHouse.Init();
            //Прокидывание взаимодействия 
            var WarHouse = NewHouse.gameObject.AddComponent<WarehouseResourcesForBuildingConstruction>();
            //Добавление одного типа ресура для постройки. 
            WarHouse.Init(NewHouse.transform, new ResourceWarhouse<Log>() { MaxElement = 10 });
            NewHouse.ConstructionBulding.EventToContact.AddListener(WarHouse.AddResource);
            WarHouse.EventFullingResource.AddListener(NewHouse.EndCreatingIcomeHouse);

        }
    }
}