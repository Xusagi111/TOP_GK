using Assets.Script.Game_Buildings.State;
using Resource;
using System.Collections.Generic;
using UnityEngine;

namespace Building
{
    //Заинжектить
    public class Buildings : MonoBehaviour
    {
        public static Buildings instance;

        public BuildingsState House1;
        public BuildingsState House2;

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