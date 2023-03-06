using Resource;
<<<<<<< Updated upstream
using TMPro;
=======
using Resourse;
>>>>>>> Stashed changes
using UnityEngine;

namespace Building
{
    public class CreatorIcome : MonoBehaviour
    {
        [field: SerializeField] public CreateResource GetResource { get; protected set; }
        [field: SerializeField] public TextMeshProUGUI  CreateTimeRes { get; protected set; }

        private float _timeOneCreateR;
        private float _timerCreateR = 3;
        private bool _isInit = false;

        public LogicContact LogicContact;

        private BaseResourse _createResource;
 
<<<<<<< Updated upstream
        public void Init(float CountTimeCreateOneResource, Transform EndMovePositionResource, BaseResource InstanceCreateResource, ResourceWarhouse resourceWarhouse, TextMeshProUGUI CreateTimeOneRes)
=======
        public void init(float CountTimeCreateOneResource, Transform EndMovePositionResource, BaseResourse InstanceCreateResource, ResourceWarhouse resourceWarhouse)
>>>>>>> Stashed changes
        {
            _timeOneCreateR = CountTimeCreateOneResource;
            _createResource = InstanceCreateResource;

            LogicContact = this.gameObject.AddComponent<LogicContact>();
            GetResource = this.gameObject.AddComponent<CreateResource>();

            GetResource.Init(EndMovePositionResource, resourceWarhouse);
            _isInit = true;
            CreateTimeRes = CreateTimeOneRes;
            _timeOneCreateR = _timerCreateR;
        }

        private void Update()
        {
            if (_isInit == false)
            {
                CreateTimeRes.text = "";
                return;
            }
            bool isCreateR = GetResource.AllResorce.AllGameObj.Count < GetResource.AllResorce.MaxElement;
            if (isCreateR)
            {
                var DifferenceTime = _timerCreateR - _timeOneCreateR;
                _timeOneCreateR = _timeOneCreateR - Time.deltaTime;
                CreateTimeRes.text = DifferenceTime >=0 ? $"{_timeOneCreateR} / {_timerCreateR}" : "";
              
                if (_timeOneCreateR <= 0)
                {
                    _timeOneCreateR = _timerCreateR;
                    CreateR();
                }
            }
            else if(isCreateR == false) CreateTimeRes.text = "Max Element";
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