using Resource;
using System.Collections.Generic;
using UnityEngine;

namespace Building
{
    [System.Serializable]
    public class ResourceWarhouse
    {
        public int MaxElement = 10;
        public List<BaseResource> AllGameObj = new List<BaseResource>();
        public readonly EnumResource TypeRes;
        public readonly Transform EndMovePositionResource;

        public ResourceWarhouse(EnumResource TypeRes, Transform EndMovePositionResource)
        {
            this.TypeRes = TypeRes;
            this.EndMovePositionResource = EndMovePositionResource;
        }
    }
}
