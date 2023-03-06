using Resourse;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceHolder : MonoBehaviour
{
    [SerializeField] private BaseResourse resourse;
    [SerializeField] private Transform pos;
    // Start is called before the first frame update
    void Start()
    {
        resourse.Jump(transform.position, pos, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
