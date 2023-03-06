using Resource;
using UnityEngine;

namespace Building
{
    public class CreatorIcome : MonoBehaviour
    {
        [field: SerializeField] public TestCreateResource GetResource { get; protected set; }
        private float _timeOneCreateR;
        private float _timerCreateR;
        private bool _isInit = false;

        public LogicContact LogicContact;

        private BaseResource _createResource;
 
        public void init(float CountTimeCreateOneResource, Transform EndMovePositionResource, BaseResource InstanceCreateResource, ResourceWarhouse resourceWarhouse)
        {
            _timeOneCreateR = CountTimeCreateOneResource;
            _createResource = InstanceCreateResource;

            LogicContact = this.gameObject.AddComponent<LogicContact>();
            GetResource = this.gameObject.AddComponent<TestCreateResource>();

            GetResource.NewInit(EndMovePositionResource, resourceWarhouse);
            _isInit = true;
        }

        private void Update()
        {
            if (_isInit == false) return;

            _timerCreateR += Time.deltaTime;
            if (_timerCreateR >= _timeOneCreateR && GetResource.AllResorce.AllGameObj.Count < GetResource.AllResorce.MaxElement)
            {
                _timerCreateR = 0;
                CreateR();
            }
             
        }

        public void GetContactResource(GameObject CheckingInventory)
        {
            Debug.LogWarning("GetContactResource");
            var Inventory = ExtensionMethodsBildings.GetInventoryUser(CheckingInventory);
            if (ExtensionMethodsBildings.CheckingNullPlayerINventory(Inventory)) return;

            var item = GetResource.AllResorce;
            if (item.AllGameObj != null && Inventory.AllResoursePlayer.Count < Inventory.MaxCountElement)
            {
                StartCoroutine(GetResource.LogicContact.GetAllResource(item, Inventory, CheckingInventory.gameObject.transform));
            }
        }
      
        private void CreateR()
        {
            GetResource.AllResorce.AllGameObj.Add(Instantiate(_createResource, GetResource.EndMovePositionResource.position, Quaternion.identity, GetResource.EndMovePositionResource.transform));
        }
    
    }
}