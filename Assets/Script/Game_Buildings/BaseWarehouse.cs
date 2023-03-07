using UnityEngine;

namespace Building
{
    public abstract class  BaseWarehouse : MonoBehaviour
    {
        [field: SerializeField] public ResourceWarhouse AllResorce { get; protected set; }

        public void Init(Transform EndMovePositionResource, ResourceWarhouse NewListRes)
        {
            if (_isinit == true) return;
            _isinit = true;
            AllResorce = NewListRes;
            InventoryContact = this.gameObject.AddComponent<InventoryContact>();
            this.EndMovePositionResource = EndMovePositionResource;
        }
    }
}