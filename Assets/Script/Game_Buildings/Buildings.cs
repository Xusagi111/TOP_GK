using UnityEngine;

namespace Building
{
    //Хранилище пребафов зданий
    public class Buildings : MonoBehaviour
    {
        public static Buildings instance;

        public DataBulding House1;
        public DataBulding House2;
        public DataBulding House3;


        private void Awake()
        {
            if (instance != null) Destroy(instance);
            instance = this;
        }
    }
}