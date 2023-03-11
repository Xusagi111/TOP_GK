using Resource;
using UnityEngine;
using Zenject;

namespace Assets.Script.Resourse
{
    public class ResourceFactory : IFactory<EnumResource, Vector3, BaseResource>
    {
        private readonly DiContainer _diContainer;
        private readonly ResourcePrefabs _resConfig;

        public ResourceFactory(DiContainer diContainer, ResourcePrefabs baseResourceConfig) { 
            _diContainer= diContainer;
            _resConfig= baseResourceConfig;
        }
        public BaseResource Create(EnumResource enumResource, Vector3 pos)
        {
            var baseResTrue = _resConfig.GetResourcePrefab(enumResource);
            return _diContainer.InstantiatePrefabForComponent<BaseResource>(baseResTrue.gameObject, pos, Quaternion.identity, parentTransform: null);
        }
    }
}
