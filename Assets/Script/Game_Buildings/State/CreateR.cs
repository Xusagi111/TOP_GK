using Building;
using Resource;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Script.Game_Buildings.State
{
    [Serializable]
    public class CreateR
    {
        [Inject]
        private List<BaseResource> AllTypeRes;

        public void CreateOneR(ResourceWarhouse ResForProduction, List<BaseResource> ListAddRes, EnumResource TypeRes)
        {
            GameObject.Destroy(ResForProduction.AllGameObj[0]);
            ListAddRes.Add(GameObject.Instantiate(GetRes(TypeRes), Vector3.zero, Quaternion.identity, ResForProduction.EndMovePositionResource));
        }

        public BaseResource GetRes(EnumResource TypeRes)
        {
            foreach (var item in AllTypeRes)
            {
                if (TypeRes == item.TypeRes) return item;
            }

            return null;
        }
    }
}
