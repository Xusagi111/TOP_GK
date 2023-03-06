using Resource;
using Resourse;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.Player.Interfaces
{
    public interface Inventory
    {
        public List<BaseResourse> AllResoursePlayer { get; set; }
        public int MaxCountElement { get;}

        public Transform Position { get; set; }
    }
}
