using Resourse;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceHolder : MonoBehaviour
{
    [SerializeField] private BaseResourse resourse;
    // Start is called before the first frame update
    void Start()
    {
        resourse.Jump(transform.position, new Vector3(1, 0, 1), 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
