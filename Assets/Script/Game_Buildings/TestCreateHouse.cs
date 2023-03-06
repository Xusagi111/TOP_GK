using Assets.Script.Player;
using Assets.Script.Player.Interfaces;
using Resource;
using UnityEngine;

namespace Building
{
    public class TestCreateHouse : MonoBehaviour
    {
        [field:SerializeField] private Transform _onePointCreateBuilding { get; set; }
        [field:SerializeField] private Transform _twoPointCreateBuilding { get; set; }
        [field:SerializeField] private Transform _threePointCreateBuilding { get; set; }

        private void Start()
        {
            //Создание игрока
            CreatePlayer();

            //Создание здания
            CreateHouseFactory(Buildings.instance.House1, EnumResource.Log, EnumResource.Log, EnumResource.Board);
            //Создания здания который производит конкретный рессурс
            CreateBuoldingsIcomeResource(Buildings.instance.House2, Buildings.instance.MoneyPrefab, EnumResource.Log, EnumResource.Money, _twoPointCreateBuilding);
            //Cоздания здания без сборки
            CreateBuildingsNoConstructRes(Buildings.instance.House2, Buildings.instance.PrefabCreateLogRes, EnumResource.Log, _threePointCreateBuilding);
        }

        private void CreateHouseFactory(DataBulding instanceHouse, EnumResource AddConstructR, EnumResource AddCreateR, EnumResource GetCreateR)
        {
            var NewHouse = Instantiate(instanceHouse, _onePointCreateBuilding.position, Quaternion.identity);
            NewHouse.Init();
          
            var Resource = new ResourceWarhouse(AddConstructR);
            var WarHouse = NewHouse.WarhouseConstruct;

            WarHouse.Init(NewHouse.transform, Resource);
            NewHouse.ConstructionBulding.EventToContact.AddListener(WarHouse.AddResource);
            WarHouse.EventFullingResource.AddListener(NewHouse.EndCreatingFactory);
            WarHouse.EventFullingResource.AddListener(() => ModificalCreatorBuildings(NewHouse, NewHouse.AddResourse, NewHouse.GetPointResource, AddCreateR, GetCreateR));
        }

        private void ModificalCreatorBuildings(DataBulding CreatorHouse, ContactWithTheObject AddResourse, ContactWithTheObject GetPointResource, EnumResource AddCreateR, EnumResource GetCreateR)
        {
            var Creator = CreatorHouse.gameObject.AddComponent<CreateResourceBuildings>();
            Creator.Init(CreatorHouse.TimeCreateOneResourceT);

            ResourceWarhouse AddResource = new ResourceWarhouse(AddCreateR);
            ResourceWarhouse GetResource = new ResourceWarhouse(GetCreateR);

            Creator.AddResourceWarhouse.Init(AddResourse.transform, AddResource);
            Creator.GetResource.Init(GetPointResource.transform, GetResource);

            Creator._checkCreateResource = true;
            GetPointResource.EventToContact.AddListener(Creator.GetContactResource);
            AddResourse.EventToContact.AddListener(Creator.AddResource);
        }

        private void CreateBuoldingsIcomeResource(DataBulding instanceHouse, BaseResource EndCreateR, EnumResource GetCreateR, EnumResource CreateR, Transform positionCreateHouse)
        {
            var NewHouse = Instantiate(instanceHouse, positionCreateHouse.position, Quaternion.identity);
            NewHouse.Init();
          
            var Resource = new ResourceWarhouse(GetCreateR);
            var WarHouse = NewHouse.WarhouseConstruct;

            WarHouse.Init(NewHouse.transform, Resource);
            NewHouse.ConstructionBulding.EventToContact.AddListener(WarHouse.AddResource);
            WarHouse.EventFullingResource.AddListener(NewHouse.EndCreatingIcomeBuildings);
            WarHouse.EventFullingResource.AddListener(() => ModificalCreatorIcomeBuildings(NewHouse, NewHouse.GetPointResource, EndCreateR, CreateR));
        }

        private void CreateBuildingsNoConstructRes(DataBulding instanceHouse, BaseResource EndCreateR, EnumResource CreateR, Transform positionCreateHouse)
        {
            var NewHouse = Instantiate(instanceHouse, positionCreateHouse.position, Quaternion.identity);
            NewHouse.EndCreatingIcomeBuildings();
            ModificalCreatorIcomeBuildings(NewHouse, NewHouse.GetPointResource, EndCreateR, CreateR);
        }

        private void ModificalCreatorIcomeBuildings(DataBulding CreatorHouse, ContactWithTheObject GetPointResource, BaseResource EndCreateR, EnumResource GetCreateR)
        {
            const int TimeCreateOneProduct = 3;
            var Creator = CreatorHouse.gameObject.AddComponent<CreatorIcome>();

            var ResourceWarhouse = new ResourceWarhouse(GetCreateR);
            Creator.Init(TimeCreateOneProduct, GetPointResource.gameObject.transform, EndCreateR, ResourceWarhouse, CreatorHouse.TimeCreateOneResourceT);
            GetPointResource.EventToContact.AddListener(Creator.GetContactResource);
        }

        private void CreatePlayer()
        {
            GameObject TestPlayer = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            TestPlayer.transform.position = new Vector3(10,0.5f);
            TestPlayer.name = "Test_PLayer";
            TestPlayer.AddComponent<Rigidbody>();
            var InvenoryPlayer = TestPlayer.AddComponent<TestPlayerInventory>();
            CreatePlayerRes(InvenoryPlayer);
        }

        private void CreatePlayerRes(Inventory InvenoryPlayer)
        {
            for (int i = 0; i < 30; i++)
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