using Resource;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.Player.Interfaces
{
    public interface Inventory
    {
        public List<BaseResource> AllResoursePlayer { get; set; }
        public int MaxCountElement { get;}

        public Transform Position { get; set; }
    }
}
