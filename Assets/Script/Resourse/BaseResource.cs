using UnityEngine;
using UnityEngine.UI;

namespace Resource
{
    public class BaseResource : MonoBehaviour
    {
        public Image Icon;
        [field: SerializeField] public EnumResource TypeRes { get; protected set; } = EnumResource.NullType;
    }
}