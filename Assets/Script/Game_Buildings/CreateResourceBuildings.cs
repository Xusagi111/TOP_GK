using Resource;
using TMPro;
using UnityEngine;

namespace Building
{
    public class CreateResourceBuildings : MonoBehaviour
    {
        [field: SerializeField] public CreateResource AddResourceWarhouse { get; protected set; }
        [field: SerializeField] public CreateResource GetResource { get; protected set; }
        public bool _checkCreateResource { get; set; } = false;
        [field: SerializeField] public TextMeshProUGUI CreateTimeResT { get; protected set; }
        [field: SerializeField] public float TimerCreateR { get; private set; } = 3;
        private float _timeOneCreateR { get; set; }


        public void Init(TextMeshProUGUI TimeCreateOneRes)
        {
            AddResourceWarhouse = this.gameObject.AddComponent<CreateResource>();
            GetResource = this.gameObject.AddComponent<CreateResource>();
            CreateTimeResT = TimeCreateOneRes;
        }

        private void Update()
        {
            if (_checkCreateResource == false) return;

            //var item = AddResourceWarhouse.AllResorce;
            //if (item.CountCreateResource <= item.CountElement &&
            //    GetResource.AllResorce.MaxElement > GetResource.AllResorce.CountElement)
            //{
            //    var DifferenceTime = TimerCreateR - _timeOneCreateR;
            //    _timeOneCreateR = _timeOneCreateR - Time.deltaTime;
            //    CreateTimeResT.text = DifferenceTime >= 0 ? $"Create R {_timeOneCreateR} / {TimerCreateR}" : "";

            //    if (_timeOneCreateR <= 0)
            //    {
            //        _timeOneCreateR = TimerCreateR;
            //        CreateResourceM(AddResourceWarhouse, GetResource);
            //    }
            //}
            //else CreateTimeResT.text = "";
        }

        public void AddResource(GameObject CheckingInventory)
        {
            var PLayerInventory = ExtensionMethodsBildings.GetInventoryUser(CheckingInventory);
            if (ExtensionMethodsBildings.CheckingNullPlayerINventory(PLayerInventory)) return;

            //var Inventory = PLayerInventory.AllResoursePlayer;
            //var item = AddResourceWarhouse.AllResorce;
            //var WarhouseR = AddResourceWarhouse.AllResorce;
            //if (WarhouseR.AllGameObj.Count < WarhouseR.MaxElement)
            //{
            //    StartCoroutine(AddResourceWarhouse.LogicContact.GetResourceInventoryToCreateProduct(item, Inventory, AddResourceWarhouse.EndMovePositionResource));
            //}
        }

        public void GetContactResource(GameObject CheckingInventory)
        {
            Debug.LogWarning("GetContactResource");
            var Inventory = ExtensionMethodsBildings.GetInventoryUser(CheckingInventory);
            if (ExtensionMethodsBildings.CheckingNullPlayerINventory(Inventory)) return;

            //var item = GetResource.AllResorce;
            //if (item.AllGameObj != null && Inventory.AllResoursePlayer.Count < Inventory.MaxCountElement)
            //{
            //    StartCoroutine(GetResource.LogicContact.GetAllResource(item, Inventory, CheckingInventory.gameObject.transform));
            //}
        }

        //private void CreateResourceM(BaseWarehouse WarhoureRes, BaseWarehouse EndWarhouseRes)
        //{
        //    var AllBaseResource = Buildings.instance.AllInstanceResource;
        //    EnumResource TypeCreateRes = EndWarhouseRes.AllResorce.TypeRes;
        //    BaseResource PrefabCreateRes = null;

        //    foreach (var item in AllBaseResource) 
        //    {
        //        if (item.TypeRes == TypeCreateRes)
        //        {
        //            PrefabCreateRes = item;
        //            break;
        //        }
        //    }

        //    var ListGetRes = WarhoureRes.AllResorce.AllGameObj;
        //    var ListSetRes = EndWarhouseRes.AllResorce.AllGameObj;

        //    if (PrefabCreateRes != null && ListGetRes.Count > 0)
        //    {
        //        var CreateR = Instantiate(PrefabCreateRes, Vector3.one, Quaternion.identity);
        //        CreateR.transform.SetParent(EndWarhouseRes.EndMovePositionResource);
        //        CreateR.transform.position = Vector3.one;
        //        var CurrentGetRes = ListGetRes[0];
        //        WarhoureRes.AllResorce.AllGameObj.Remove(CurrentGetRes);
        //        Destroy(CurrentGetRes.gameObject);
        //        ListSetRes.Add(CreateR);
        //        EndWarhouseRes.AllResorce.CountElement = ListSetRes.Count;
        //    }
        //    else Debug.LogError("Данный рессурс равен null");
        //}
    }
}
