using Building;
using Resource;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Script.Game_Buildings.State
{
    public static class UpdateTimeCreateR 
    {
        public static IEnumerator UpdateTime(ResourceWarhouse ResForProduction, List<BaseResource> ListAddRes, EnumResource ReceivedRes,
            float TImeCreateOneRes, TextMeshProUGUI TextCountCreateR)
        {
            float InitialProductionTime = TImeCreateOneRes;
            while (TImeCreateOneRes > 0)
            {
                TextCountCreateR.text = $"{TImeCreateOneRes} / {InitialProductionTime}";
                TImeCreateOneRes -= 1;
                yield return new WaitForSeconds(1f);
            }
            TextCountCreateR.text = "";
            if (TImeCreateOneRes == 0)
            {
                CreateR.CreateOneR(ResForProduction, ListAddRes, ReceivedRes);
            }
        }
    }

    public static class CreateR
    {
        public static void CreateOneR(ResourceWarhouse ResForProduction, List<BaseResource> ListAddRes, EnumResource TypeRes)
        {
            BaseResource PrefabCreateR = null;
            foreach (var item in Buildings.instance.AllInstanceResource)
            {
                if (TypeRes == item.TypeRes) { PrefabCreateR = item; break;} 
            }

            GameObject.Destroy(ResForProduction.AllGameObj[0]);
            ListAddRes.Add(GameObject.Instantiate(PrefabCreateR, Vector3.zero, Quaternion.identity, ResForProduction.EndMovePositionResource));
        }
    }
}