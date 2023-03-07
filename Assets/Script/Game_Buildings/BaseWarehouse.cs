using UnityEngine;

namespace Building
{
    public abstract class  BaseWarehouse : MonoBehaviour
    {
        [field: SerializeField] public ResourceWarhouse AllResorce { get; protected set; }
    }
}