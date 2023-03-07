using UnityEngine;
using Zenject;

namespace Assets.Script.Game_Buildings
{
    public class PointCreateBuilbing : MonoInstaller
    {
        [field: SerializeField] public Transform _onePointCreateBuilding { get; set; }
        [field: SerializeField] private Transform _twoPointCreateBuilding { get; set; }
        [field: SerializeField] private Transform _threePointCreateBuilding { get; set; }

        //Проверить на то, как работает
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<PointCreateBuilbing>().AsSingle();
        }
    }
}