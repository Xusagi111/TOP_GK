using UnityEngine;

namespace Building
{
    //Рассположение основных точек взаимодействия на префабе 
    public class DataBulding : MonoBehaviour //InstanseToPrefab
    {
        [field: SerializeField] public ContactWithTheObject ConstructionBulding { get; private set; }
        [field: SerializeField] public ContactWithTheObject СollectionPointResource { get; private set; }
        [field: SerializeField] public ContactWithTheObject GetCreateResourse { get; private set; }
        [field: SerializeField] public GameObject FinalView { get; private set; } //Финальный вид здания, изначально он будет не построен.
        [field: SerializeField] public float TimeCreatingSingleUnit { get; private set; }

        //Class Где должен быть содержан ui который в данный момент отобрается.
        public void Init()
        {
            ConstructionBulding.gameObject.SetActive(true);
            СollectionPointResource.gameObject.SetActive(false);
            GetCreateResourse.gameObject.SetActive(false);
        }

        public void EndCreatingFactory()
        {
            ConstructionBulding.gameObject.SetActive(false);
            СollectionPointResource.gameObject.SetActive(true);
            GetCreateResourse.gameObject.SetActive(true);
        }
        
        public void EndCreatingIcomeHouse()
        {
            ConstructionBulding.gameObject.SetActive(false);
            GetCreateResourse.gameObject.SetActive(true);
        }
    }
}