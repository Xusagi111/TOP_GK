using Resourse;
using UnityEngine;

namespace Building
{
    public abstract class BaseWarehouse : MonoBehaviour
    {
        [field: SerializeField] public ResourceWarhouse<Log> LogResource { get; protected set; }
        [field: SerializeField] public ResourceWarhouse<Board> BoardResource { get; protected set; }
    }
}