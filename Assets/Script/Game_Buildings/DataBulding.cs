using TMPro;
using UnityEngine;


namespace Building
{
    public class DataBulding : MonoBehaviour 
    {
        //Прокинуть триггеры через UNiRX
        [field: SerializeField] public Collider ConstructionBulding { get; private set; }
        [field: SerializeField] public Collider GetRes { get; private set; }
        [field: SerializeField] public Collider AddRes { get; private set; }
        [field: SerializeField] public TextMeshProUGUI TimeCreateOneResourceT { get; private set; }

        public void ConstructViewBuilding()
        {
            ConstructionBulding.gameObject.SetActive(true);
            GetRes.gameObject.SetActive(false);
            AddRes.gameObject.SetActive(false);
        }

        public void EndViewFactory() 
        {
            ConstructionBulding.gameObject.SetActive(false);
            GetRes.gameObject.SetActive(true);
            AddRes.gameObject.SetActive(true);
        }
        
        public void EndViewCreatingIcomeBuildings()
        {
            ConstructionBulding.gameObject.SetActive(false);
            GetRes.gameObject.SetActive(true);
        }

        public void DisableView()
        {
            ConstructionBulding.gameObject.SetActive(false);
            GetRes.gameObject.SetActive(false);
            AddRes.gameObject.SetActive(false);
        }
    }
}
