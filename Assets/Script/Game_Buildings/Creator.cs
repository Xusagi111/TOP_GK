using UnityEngine;

namespace Building
{
    public class Creator : MonoBehaviour
    {
        public E Create<T, E>(T GetResourse) where E : Component
        {
            var CreateObj = new GameObject();
            return CreateObj.AddComponent<E>();
        }
    }
}
