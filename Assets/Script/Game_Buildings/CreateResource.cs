using Assets.Script.Player;
using Building;
using Resource;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Building
{
    public class CreateResource : MonoBehaviour
    {
        [field:SerializeField] public TestCreateResource AddResorseWArhouse { get; set; }
        [field: SerializeField] public TestCreateResource GetResource { get; set; }
        [field: SerializeField] public UnityEvent EventFullingResource { get; set; } = new UnityEvent();

        public void Init()
        {
            AddResorseWArhouse =  this.gameObject.AddComponent<TestCreateResource>();
            GetResource =  this.gameObject.AddComponent<TestCreateResource>();
        }

        private void FixedUpdate()
        {
            if (AddResorseWArhouse.LogResource != null && AddResorseWArhouse.LogResource.CountElement > 0)
            {
                AddResorseWArhouse.LogResource.CountElement--;
                Create<Log, Board>(AddResorseWArhouse.LogResource, GetResource.BoardResource);
            }

            //if (AddResorseWArhouse.BoardResource != null && AddResorseWArhouse.BoardResource.CountElement > 0)
            //{
            //    AddResorseWArhouse.BoardResource.CountElement--;
            //    Create<Board>(GetResource.BoardResource);
            //}
        }

        private void OnDestroy() => EventFullingResource.RemoveAllListeners();

        public void AddResource(GameObject CheckingInventory)
        {
            Debug.LogWarning("AddResource");

            var Inventory = GetInventoryUser(CheckingInventory);
            if (Inventory == null)
            {
                Debug.LogError("Ошибка у игрока не найден инвентарь");
                return;
            }

            //Нужно определить какие из ресурсов являются производимыми, а какие забираемыми 
            if (Inventory != null)
            {
                if (AddResorseWArhouse.LogResource != null && AddResorseWArhouse.LogicContact != null) AddResorseWArhouse.LogicContact.StartCoroutine(
                    AddResorseWArhouse.LogicContact.GetResourceInventoryToCreateProduct<Log>(AddResorseWArhouse.LogResource, Inventory, AddResorseWArhouse.EndMovePositionResource));

                if (AddResorseWArhouse.BoardResource != null && AddResorseWArhouse.LogicContact != null) AddResorseWArhouse.LogicContact.StartCoroutine(
                   AddResorseWArhouse.LogicContact.GetResourceInventoryToCreateProduct<Board>(AddResorseWArhouse.BoardResource, Inventory, AddResorseWArhouse.EndMovePositionResource));
            }
        }

        public void GetContactResource(GameObject CheckingInventory)
        {
            Debug.LogWarning("GetContactResource");
            var Inventory = GetInventoryUser(CheckingInventory);
            if (Inventory == null)
            {
                Debug.LogError("Ошибка у игрока не найден инвентарь");
                return;
            }

            if (GetResource.LogResource?.AllGameObj != null && GetResource.LogResource.CountElement != 0)
            {
                GetResource.LogicContact.StartCoroutine(
                     GetResource.LogicContact.GetAllResource<Log>(GetResource.LogResource, Inventory, CheckingInventory.gameObject.transform));
            } 

            if (GetResource.BoardResource?.AllGameObj != null && GetResource.BoardResource.CountElement != 0)
            {
                GetResource.LogicContact.StartCoroutine(
                     GetResource.LogicContact.GetAllResource<Board>(GetResource.BoardResource, Inventory, CheckingInventory.gameObject.transform));
            }
        }

        private void Create<E, T>(ResourceWarhouse<E> Warhouse,  ResourceWarhouse<T> resourceWarhouse)
        {
            var BaseRes = Buildings.instance.AllInstanceResource;
            foreach (var item in BaseRes)
            {
                if (item is T )
                {
                    //Прикрепить к нужному гейм объекту.
                    var CreateR = Instantiate(item, Vector3.one, Quaternion.identity);
                    CreateR.transform.SetParent(GetResource.EndMovePositionResource);
                    resourceWarhouse.AllGameObj.Add(CreateR.GetComponent<T>());
                    Warhouse.CountElement--;
                    resourceWarhouse.CountElement++;
                }
            }
        }

        private List<BaseResource> GetInventoryUser(GameObject CheckingInventory) 
        {
            var PLayerInventory = CheckingInventory.GetComponent<TestPlayerInventory>();
            if (PLayerInventory == null) return null;
            else return PLayerInventory.AllResoursePlayer;
        }
    }
}


public class CreatorIcome : MonoBehaviour
{
    private float _timeOneCreateR;
    private float _timerCreateR;
    private bool _isInit = false;

    public LogicContact LogicContact;
    public Transform EndMovePositionResource;

    public void init(float CountTimeCreateOneResource, Transform EndMovePositionResource)
    {
        _timeOneCreateR = CountTimeCreateOneResource;
        LogicContact = this.gameObject.AddComponent<LogicContact>();
        this.EndMovePositionResource = EndMovePositionResource;
        _isInit = true;
    }

    private void Update()
    {
        if (_isInit == false) return;

        _timerCreateR += Time.deltaTime;
        if (_timerCreateR >= _timeOneCreateR)
        {
            _timerCreateR = 0;
            CreateR();
        }
    }

    private void CreateR()
    {
        Instantiate(Buildings.instance.MoneyPrefab, EndMovePositionResource.position, Quaternion.identity, EndMovePositionResource.transform);
    }
}

