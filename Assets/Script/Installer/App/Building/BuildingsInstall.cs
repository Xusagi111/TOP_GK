using Assets.Script.Game_Buildings.State;
using Assets.Script.Game_Buildings.State.NewState;
using Resource;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Building
{
    [CreateAssetMenu(fileName = "BuildingsIngect", menuName = "SOInstaller/BuildingsIngect")]
    public class BuildingsInstall : ScriptableObjectInstaller<BuildingsInstall>
    {
        [field: SerializeField] public BuildingsStateLog House1 { get; private set; }
        [field: SerializeField] public BuildingsStateLog House2 { get; private set; }

        [field: SerializeField] public List<BaseResource> AllInstanceResource{ get; private set; }

        //Возможно в дальнейщем использовать компонент с помощью листа.
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<List<BaseResource>>().FromInstance(AllInstanceResource).AsSingle();
            Container.BindInterfacesAndSelfTo<BuildingsStateLog>().FromInstance(House1).AsCached();
            //Container.BindInterfacesAndSelfTo<BuildingsStateLog>().FromInstance(House2).AsCached();

            InstallinitCreateComponent();
        }

        private void InstallinitCreateComponent()
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