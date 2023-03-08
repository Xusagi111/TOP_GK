using UnityEngine;
using Zenject;

namespace Assets.Script.Installer.App.Building
{
    [CreateAssetMenu(fileName = "ConfigBuilding", menuName = "SOInstaller/ConfigBuilding")]
    public class ConfigBuilding : ScriptableObjectInstaller<ConfigBuilding>
    {
        [SerializeField] private float _timeCreateOneRes = 2;
        [SerializeField] private float _countResProducedatTime = 2;

        public float TimeCreateOneRes => _timeCreateOneRes;
        public float CountResProducedatTime => _countResProducedatTime;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ConfigBuilding>().FromInstance(this).AsSingle();
        }
    }
}
