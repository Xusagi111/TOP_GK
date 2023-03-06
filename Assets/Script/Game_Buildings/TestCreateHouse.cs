using Assets.Script.Player;
using Resource;
using System.Collections.Generic;
using UnityEngine;

namespace Building
{
    public class TestCreateHouse : MonoBehaviour
    {
        [field:SerializeField] private Transform _onePointCreateBuilding { get; set; }
        [field:SerializeField] private Transform _twoPointCreateBuilding { get; set; }

        private void Start()
        {
            //Создание игрока
            CreatePlayer();

            //Создание здания
            CreateHouseFactory(Buildings.instance.House1, EnumResource.Log, EnumResource.Log, EnumResource.Board);
            //Создания здания который производит конкретный рессурс
            CreateHouseIcomeResource(Buildings.instance.House2, Buildings.instance.MoneyPrefab, EnumResource.Log);
        }

        private void CreateHouseFactory(DataBulding instanceHouse, EnumResource AddConstructR, EnumResource AddCreateR, EnumResource GetCreateR)
        {
            var NewHouse = Instantiate(instanceHouse, _onePointCreateBuilding.position, Quaternion.identity);
            NewHouse.Init();
            var WarHouse = NewHouse.WarhouseConstruct;
            //Добавление одного типа ресура для постройки. 
            var Resource = new ResourceWarhouse(AddConstructR);

            WarHouse.NewInit(NewHouse.transform, Resource);
            NewHouse.ConstructionBulding.EventToContact.AddListener(WarHouse.AddResource);
            WarHouse.EventFullingResource.AddListener(NewHouse.EndCreatingFactory);
            WarHouse.EventFullingResource.AddListener(() => ModificalCreatorHouse(NewHouse, NewHouse.AddResourse, NewHouse.GetPointResource, AddCreateR, GetCreateR));
        }

        private void ModificalCreatorHouse(DataBulding CreatorHouse, ContactWithTheObject AddResourse, ContactWithTheObject GetPointResource, EnumResource AddCreateR, EnumResource GetCreateR)
        {
            var Creator = CreatorHouse.gameObject.AddComponent<CreateResource>();
            Creator.Init();

            ResourceWarhouse AddResource = new ResourceWarhouse(AddCreateR);
            ResourceWarhouse GetResource = new ResourceWarhouse(GetCreateR);

            Creator.AddResourceWarhouse.NewInit(AddResourse.transform, AddResource);
            Creator.GetResource.NewInit(GetPointResource.transform, GetResource);

            Creator._checkCreateResource = true;
            GetPointResource.EventToContact.AddListener(Creator.GetContactResource);
            AddResourse.EventToContact.AddListener(Creator.AddResource);
        }


        private void CreateHouseIcomeResource(DataBulding instanceHouse, BaseResource EndCreateR, EnumResource GetCreateR)
        {
            var NewHouse = Instantiate(instanceHouse, _twoPointCreateBuilding.position, Quaternion.identity);
            NewHouse.Init();
            var WarHouse = NewHouse.WarhouseConstruct;
            //Добавление одного типа ресура для постройки. 
            var Resource = new ResourceWarhouse(GetCreateR);

            WarHouse.NewInit(NewHouse.transform, Resource);
            NewHouse.ConstructionBulding.EventToContact.AddListener(WarHouse.AddResource);
            WarHouse.EventFullingResource.AddListener(NewHouse.EndCreatingIcomeHouse);
            WarHouse.EventFullingResource.AddListener(() => ModificalCreatorIcomeHouse(NewHouse, NewHouse.GetPointResource, EndCreateR, GetCreateR));
        }

        private void ModificalCreatorIcomeHouse(DataBulding CreatorHouse, ContactWithTheObject GetPointResource, BaseResource EndCreateR, EnumResource GetCreateR)
        {
            var Creator = CreatorHouse.gameObject.AddComponent<CreatorIcome>();
            const int TimeCreateOneProduct = 3;
            var ResourceWarhouse = new ResourceWarhouse(GetCreateR);
            Creator.init(TimeCreateOneProduct, GetPointResource.gameObject.transform, EndCreateR, ResourceWarhouse);
            GetPointResource.EventToContact.AddListener(Creator.GetContactResource);
        }

        private void CreatePlayer()
        {
            GameObject TestPlayer = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            TestPlayer.transform.position = new Vector3(10,0.5f);
            TestPlayer.name = "Test_PLayer";
            TestPlayer.AddComponent<Rigidbody>();
            var InvenoryPlayer = TestPlayer.AddComponent<TestPlayerInventory>();

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