using Assets.Script.Player;
using Assets.Script.Player.Interfaces;
using Resource;
using UnityEngine;
using Zenject;

namespace Building
{
    public class EntryPointScene : MonoBehaviour
    {
        private void Start()
        {
            //Создание игрока
            CreatePlayer();
            //Заменить на точки которые должны инжектиться.
            var StateBuildingHouse1 = Instantiate(Buildings.instance.House1, Vector3.zero, Quaternion.identity);
            StateBuildingHouse1.SetConstruct();
            //Создание здания
            //CreateHouseFactory(Buildings.instance.House1, EnumResource.Log, EnumResource.Log, EnumResource.Board);
            ////Создания здания который производит конкретный рессурс
            //CreateBuoldingsIcomeResource(Buildings.instance.House2, Buildings.instance.MoneyPrefab, EnumResource.Log, EnumResource.Money, _twoPointCreateBuilding);
            ////Cоздания здания без сборки
            //CreateBuildingsNoConstructRes(Buildings.instance.House2, Buildings.instance.PrefabCreateLogRes, EnumResource.Log, _threePointCreateBuilding);
        }

        //private void CreateHouseFactory(DataBulding instanceHouse, EnumResource AddConstructR, EnumResource AddCreateR, EnumResource GetCreateR)
        //{
        //    var NewHouse = Instantiate(instanceHouse, _onePointCreateBuilding.position, Quaternion.identity);
        //    NewHouse.ConstructViewBuilding();
          
        //    var Resource = new ResourceWarhouse(AddConstructR);
        //    var WarHouse = NewHouse.WarhouseConstruct;

        //    //WarHouse.Init(NewHouse.transform, Resource);
        //    NewHouse.ConstructionBulding.EventToContact.AddListener(WarHouse.AddResource);
        //    WarHouse.EventFullingResource.AddListener(NewHouse.EndCreatingFactory);
        //    WarHouse.EventFullingResource.AddListener(() => ModificalCreatorBuildings(NewHouse, NewHouse.AddRes, NewHouse.GetRes, AddCreateR, GetCreateR));
        //}

        //private void ModificalCreatorBuildings(DataBulding CreatorHouse, ContactWithTheObject AddResourse, ContactWithTheObject GetPointResource, EnumResource AddCreateR, EnumResource GetCreateR)
        //{
        //    var Creator = CreatorHouse.gameObject.AddComponent<CreateResourceBuildings>();
        //    Creator.Init(CreatorHouse.TimeCreateOneResourceT);

        //    ResourceWarhouse AddResource = new ResourceWarhouse(AddCreateR);
        //    ResourceWarhouse GetResource = new ResourceWarhouse(GetCreateR);

        //    Creator.AddResourceWarhouse.Init(AddResourse.transform, AddResource);
        //    Creator.GetResource.Init(GetPointResource.transform, GetResource);

        //    Creator._checkCreateResource = true;
        //    GetPointResource.EventToContact.AddListener(Creator.GetContactResource);
        //    AddResourse.EventToContact.AddListener(Creator.AddResource);
        //}

        //private void CreateBuoldingsIcomeResource(DataBulding instanceHouse, BaseResource EndCreateR, EnumResource GetCreateR, EnumResource CreateR, Transform positionCreateHouse)
        //{
        //    var NewHouse = Instantiate(instanceHouse, positionCreateHouse.position, Quaternion.identity);
        //    NewHouse.ConstructViewBuilding();
          
        //    var Resource = new ResourceWarhouse(GetCreateR);
        //    var WarHouse = NewHouse.WarhouseConstruct;

        //    //WarHouse.Init(NewHouse.transform, Resource);
        //    NewHouse.ConstructionBulding.EventToContact.AddListener(WarHouse.AddResource);
        //    WarHouse.EventFullingResource.AddListener(NewHouse.EndCreatingIcomeBuildings);
        //    WarHouse.EventFullingResource.AddListener(() => ModificalCreatorIcomeBuildings(NewHouse, NewHouse.GetRes, EndCreateR, CreateR));
        //}

        //private void CreateBuildingsNoConstructRes(DataBulding instanceHouse, BaseResource EndCreateR, EnumResource CreateR, Transform positionCreateHouse)
        //{
        //    var NewHouse = Instantiate(instanceHouse, positionCreateHouse.position, Quaternion.identity);
        //    NewHouse.EndViewCreatingIcomeBuildings();
        //    ModificalCreatorIcomeBuildings(NewHouse, NewHouse.GetRes, EndCreateR, CreateR);
        //}

        //private void ModificalCreatorIcomeBuildings(DataBulding CreatorHouse, ContactWithTheObject GetPointResource, BaseResource EndCreateR, EnumResource GetCreateR)
        //{
        //    const int TimeCreateOneProduct = 3;
        //    var Creator = CreatorHouse.gameObject.AddComponent<CreatorIcome>();

        //    var ResourceWarhouse = new ResourceWarhouse(GetCreateR);
        //    Creator.Init(TimeCreateOneProduct, GetPointResource.gameObject.transform, EndCreateR, ResourceWarhouse, CreatorHouse.TimeCreateOneResourceT);
        //    GetPointResource.EventToContact.AddListener(Creator.GetContactResource);
        //}

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
            var BuildingsIns = Buildings.instance;
            BaseResource PrefabLogRes = null;
            BaseResource PrefabBoardRes = null;
            foreach (var item in BuildingsIns.AllInstanceResource)
            {
                if (item.TypeRes == EnumResource.Log) PrefabLogRes = item;
                if (item.TypeRes == EnumResource.Board) PrefabBoardRes = item;
            }

            for (int i = 0; i < 30; i++)
            {
                var BaseRes = Instantiate(PrefabLogRes, new Vector3(15, 0.5f,1f), Quaternion.identity);
                BaseRes.name = "Log";
                InvenoryPlayer.AllResoursePlayer.Add(BaseRes);
            }

            for (int i = 0; i < 5; i++)
            {
                var BaseRes = Instantiate(PrefabBoardRes, new Vector3(15, 0.5f, 1f), Quaternion.identity);
                BaseRes.name = "Board";
                InvenoryPlayer.AllResoursePlayer.Add(BaseRes);
            }
        }
    }
}

//public class Player
//{
//}

//public class Enemy
//{
//    readonly Player _player;

//    public Enemy(Player player)
//    {
//        _player = player;
//    }

//    public class Factory : PlaceholderFactory<Enemy>
//    {
//    }
//}

//public class EnemySpawner : ITickable
//{
//    readonly Enemy.Factory _enemyFactory;

//    public EnemySpawner(Enemy.Factory enemyFactory)
//    {
//        _enemyFactory = enemyFactory;
//    }

//    public void Tick()
//    {
//        if (ShouldSpawnNewEnemy())
//        {
//            var enemy = _enemyFactory.Create();
//            // ...
//        }
//    }
//}

//public class TestInstaller : MonoInstaller
//{
//    public override void InstallBindings()
//    {
//        Container.BindInterfacesTo<EnemySpawner>().AsSingle();
//        Container.BindFactory<Enemy, Enemy.Factory>();
//    }
//}