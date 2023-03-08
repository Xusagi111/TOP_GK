using Building;
using Resource;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Script.Game_Buildings.State
{
    [Serializable]
    public class UpdateTimeCreateR
    {
        [Inject]
        private CreateR createR;

        public IEnumerator UpdateTime(LinkCoroutine linkCoroutine, ResourceWarhouse ResForProduction, List<BaseResource> ListAddRes, EnumResource ReceivedRes,
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
                createR.CreateOneR(ResForProduction, ListAddRes, ReceivedRes);
            }

           linkCoroutine.UpdateTimeCor = null;
        }
    }
}