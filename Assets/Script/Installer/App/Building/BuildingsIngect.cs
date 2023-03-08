using Assets.Script.Game_Buildings;
using Assets.Script.Game_Buildings.State;
using Assets.Script.Game_Buildings.State.NewState;
using Resource;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Building
{
    [CreateAssetMenu(fileName = "BuildingsIngect", menuName = "SOInstaller/BuildingsIngect")]
    public class BuildingsIngect : ScriptableObjectInstaller<BuildingsIngect>
    {
        [field: SerializeField] public BuildingsStateLog House1 { get; private set; }
        [field: SerializeField] public BuildingsStateLog House2 { get; private set; }

        [field: SerializeField] public List<BaseResource> AllInstanceResource{ get; private set; }

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<List<BaseResource>>().FromInstance(AllInstanceResource).AsSingle();
            installCreateComponent();
            //var OneHouse =  Container.InstantiatePrefabForComponent<BuildingsStateLog>(House1, Vector3.zero, Quaternion.identity, null);
            Container.BindInterfacesAndSelfTo<BuildingsStateLog>().FromInstance(House1).AsCached();
        }

        private void installCreateComponent()
        {
            var a = new CreateR();
            var b = new UpdateTimeCreateR();
            Container.BindInstance(a);
            Container.BindInstance(b);

            Container.QueueForInject(a);
            Container.QueueForInject(b);
        }
    }
}