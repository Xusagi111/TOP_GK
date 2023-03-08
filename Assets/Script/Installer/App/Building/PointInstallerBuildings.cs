using UnityEngine;
using Zenject;

namespace Assets.Script.Game_Buildings
{
    public class PointInstallerBuildings : MonoInstaller
    {
        [field: SerializeField] public Transform _onePointCreateBuilding;
        [field: SerializeField] private Transform _twoPointCreateBuilding;
        [field: SerializeField] private Transform _threePointCreateBuilding;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<PointInstallerBuildings>().AsSingle();
        }
    }
}