using Building;
using Resource;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Script.Game_Buildings.State
{
    //Перенаполнил логикой, должно быть только создание
    //Внести его в контейнер
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
        //Сделать производимый рессурс с помощью перебора массива.
        //Убрать дефолтный тип
        public static void CreateOneR(ResourceWarhouse ResForProduction, List<BaseResource> ListAddRes, EnumResource resource)
        {
            GameObject.Destroy(ResForProduction.AllGameObj[0]);
            ListAddRes.Add(GameObject.Instantiate(new BaseResource(), Vector3.zero, Quaternion.identity, ResForProduction.EndMovePositionResource));
        }
    }
}