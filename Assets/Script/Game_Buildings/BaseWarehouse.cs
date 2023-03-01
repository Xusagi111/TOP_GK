using Resource;
using UnityEngine;

namespace Building
{
    public abstract class BaseWarehouse : MonoBehaviour
    {
        [field: SerializeField] public ResourceWarhouse<Log> LogResource { get; protected set; }
        [field: SerializeField] public ResourceWarhouse<Board> BoardResource { get; protected set; }

        public LogicContact LogicContact;
        public Transform EndMovePositionResource;
        private bool _isinit { get; set; } = false;

        public void Init(Transform EndMovePositionResource, ResourceWarhouse<Log> LogRes = null, ResourceWarhouse<Board> BoardRes = null)
        {
            if (_isinit == true) return;
            _isinit = true;
            LogResource = LogRes;
            BoardResource = BoardRes;
            LogicContact = this.gameObject.AddComponent<LogicContact>();
            this.EndMovePositionResource = EndMovePositionResource;
        }
    }
}