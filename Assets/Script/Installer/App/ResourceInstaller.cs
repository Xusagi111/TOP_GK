using Assets.Script.Resourse;
using Resource;
using System;
using System.Linq;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ResourceSOInstaller", menuName = "SOInstallers/ResourceSOInstaller")]
public class ResourceInstaller : ScriptableObjectInstaller<ResourceInstaller>
{
    [SerializeField] private ResourcePrefabs prefabs;
    public override void InstallBindings()
    {
        Container.Bind<ResourcePrefabs>().FromInstance(prefabs);

        Container
            .BindFactory<EnumResource, Vector3, BaseResource, BaseResource.Factory>()
            .FromFactory<ResourceFactory>();

        Container.Bind<ResourceSpawner>().AsSingle();
    }
}

[Serializable]
public class ResourcePrefabs
{
    public BaseResource[] baseResources;

    public BaseResource GetResourcePrefab(EnumResource enumResource) =>
        baseResources.FirstOrDefault(x => x.TypeRes == enumResource);
}