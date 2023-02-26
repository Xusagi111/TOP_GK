using Assets.Script.Player;
using Resourse;
using UnityEngine;

namespace Building
{
    public class TestCreateHouse : MonoBehaviour
    {
        [field:SerializeField] private Transform _onePointCreateBuilding { get; set; }

        private void Start()
        {
            //Создание игрока
            CreatePlayer();

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

        private void CreatePlayer()
        {
            GameObject TestPlayer = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            TestPlayer.transform.position = new Vector3(10,0.5f);
            TestPlayer.name = "Test_PLayer";
            TestPlayer.AddComponent<Rigidbody>();
            var InvenoryPlayer = TestPlayer.AddComponent<TestPlayerInventory>();

            for (int i = 0; i < 5; i++)
            {
                GameObject NewGameOj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                NewGameOj.transform.position = new Vector3(15, 0.5f);
                NewGameOj.name = "Log";
                InvenoryPlayer.AllResoursePlayer.Add(NewGameOj.AddComponent<Log>());
            }

            for (int i = 0; i < 5; i++)
            {
                GameObject NewGameOj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                NewGameOj.transform.position = new Vector3(15, 0.5f);
                NewGameOj.name = "Board";
                InvenoryPlayer.AllResoursePlayer.Add(NewGameOj.AddComponent<Board>());
            }
        }
    }
}