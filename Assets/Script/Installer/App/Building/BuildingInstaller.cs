using System;
using UnityEngine;
using Zenject;

namespace Assets.Script.Installer.App.Building
{
    [CreateAssetMenu(fileName = "BuildingInstaller", menuName = "SOInstaller/BuildingInstaller")]
    public class BuildingInstaller : ScriptableObjectInstaller<BuildingInstaller>
    {
        [SerializeField] private PlayerModel playerModel;
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private Vector3 startPos = Vector3.zero;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerModel>().FromInstance(playerModel).AsSingle();
            BindPlayerView();
            BindPlayerPresenter();
        }

        public void BindPlayerView()
        {
            PlayerView playerView = Container.
                    InstantiatePrefabForComponent<PlayerView>(playerPrefab, startPos, Quaternion.identity, null);

            Container.
                BindInterfacesAndSelfTo<PlayerView>().
                FromInstance(playerView).AsSingle();
        }

        public void BindPlayerPresenter()
        {
            Container.
                Bind(typeof(ITickable), typeof(IInitializable), typeof(IDisposable), typeof(IPlayerPresenter)).
                To<PlayerPresenter>().AsSingle();
        }

    }
}
