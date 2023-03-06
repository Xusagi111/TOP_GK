using Resource;
using UnityEngine;

public class ResourceHolder : MonoBehaviour
{
    [SerializeField] private BaseResource resourse;
    [SerializeField] private Transform tr;
    // Start is called before the first frame update
    void Start()
    {
        resourse.Jump(new Vector3(3,3,3), tr, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
