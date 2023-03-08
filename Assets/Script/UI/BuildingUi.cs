using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script.UI
{
    public class BuildingUi: MonoBehaviour
    {
        [SerializeField] private Image _constructI;
        [SerializeField] private Image _getResI;
        [SerializeField] private Image _setResI;

        [SerializeField] private TextMeshProUGUI _constructT;
        [SerializeField] private TextMeshProUGUI _getResT;
        [SerializeField] private TextMeshProUGUI _setResT;

        public Image ConstructI => ConstructI;
        public Image GetResI => _getResI;
        public Image SetResI => _setResI;

        public TextMeshProUGUI ConstructT => _constructT;
        public TextMeshProUGUI GetResT => _getResT;
        public TextMeshProUGUI SetResT => _setResT;
    }
}