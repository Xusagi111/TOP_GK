using Resource;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner
{
    private readonly BaseResource.Factory resfactory;

    public ResourceSpawner(BaseResource.Factory factory)
    {
        resfactory= factory;
    }

    public BaseResource Create(EnumResource enumResource, Vector3 pos) => resfactory.Create(enumResource, pos);

    public BaseResource[] CreateList(int count, EnumResource enumResource, Vector3 pos)
    {
        BaseResource[] list = new BaseResource[count];
        for(int i =0; i<count; i++)
            list[i] = Create(enumResource, pos);

        return list; 
    }
}
