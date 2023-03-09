using Resource;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Building
{
    public abstract class BaseWarehouse : MonoBehaviour
    {
        [SerializeField] private ResourceWarhouse[] _resourcesPlaces;
        private Dictionary<EnumResource, ResourceWarhouse> resWarhouses = new Dictionary<EnumResource, ResourceWarhouse>();
        public void GetInventory()
        {

        }
        public void InitResWarhouses()
        {
            for(int i = 0; i< _resourcesPlaces.Length; i++)
                resWarhouses.Add(_resourcesPlaces[i].EnumResource, _resourcesPlaces[i]);
        }
    }
}