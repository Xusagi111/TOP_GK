using UnityEngine;

namespace Building
{
    public abstract class  BaseWarehouse : MonoBehaviour
    {
        [field: SerializeField] public ResourceWarhouse AllResorce { get; protected set; }
        public InventoryContact LogicContact;
        public Transform EndMovePositionResource;
        private bool _isinit { get; set; } = false;

        public void Init(Transform EndMovePositionResource, ResourceWarhouse NewListRes)
        {
            if (_isinit == true) return;
            _isinit = true;
            AllResorce = NewListRes;
            LogicContact = this.gameObject.AddComponent<InventoryContact>();
            this.EndMovePositionResource = EndMovePositionResource;
        }
    }
}