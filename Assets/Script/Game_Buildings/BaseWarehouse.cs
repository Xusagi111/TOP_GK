using UnityEngine;

namespace Building
{
    public class  BaseWarehouse : MonoBehaviour
    {
        [field: SerializeField] public ResourceWarhouse AllResorce { get; protected set; }
    }
}