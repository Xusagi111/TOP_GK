using UnityEngine;

namespace Building
{
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
}