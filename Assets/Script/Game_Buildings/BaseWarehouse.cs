using UnityEngine;

namespace Building
{
    public abstract class  BaseWarehouse : MonoBehaviour
    {
        [field: SerializeField] public ResourceWarhouse AllResorce { get; protected set; }
        public LogicContact LogicContact;
        public Transform EndMovePositionResource;
        private bool _isinit { get; set; } = false;

        public void NewInit(Transform EndMovePositionResource, ResourceWarhouse NewListRes)
        {
            if (_isinit == true) return;
            _isinit = true;
            AllResorce = NewListRes;
            LogicContact = this.gameObject.AddComponent<LogicContact>();
            this.EndMovePositionResource = EndMovePositionResource;
        }
    }
}