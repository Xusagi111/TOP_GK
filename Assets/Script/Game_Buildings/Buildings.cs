using Assets.Script.Resourse;
using Resource;
using System.Collections.Generic;
using UnityEngine;

namespace Building
{
    //????????? ????????
    public class Buildings : MonoBehaviour
    {
        public static Buildings instance;

        public DataBulding House1;
        public DataBulding House2;

        public MoneyObj MoneyPrefab;
        public BaseResource PrefabCreateLogRes;

        public List<BaseResource> AllInstanceResource;

        private void Awake()
        {
            if (instance != null) Destroy(instance);
            instance = this;
        }
    }
}