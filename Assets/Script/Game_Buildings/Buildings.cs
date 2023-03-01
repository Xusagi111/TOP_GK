using Assets.Script.Resourse;
using Resource;
using System.Collections.Generic;
using UnityEngine;

namespace Building
{
    //Хранилище пребафов
    public class Buildings : MonoBehaviour
    {
        public static Buildings instance;

        public DataBulding House1;
        public DataBulding House2;
        public DataBulding House3;

        public MoneyObj MoneyPrefab;

        public List<BaseResource> AllInstanceResource;

        private void Awake()
        {
            if (instance != null) Destroy(instance);
            instance = this;
        }
    }
}