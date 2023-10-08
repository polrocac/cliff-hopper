using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Normalize : MonoBehaviour
{

    MeshFilter mf;

    // Start is called before the first frame update
    void Awake()
    {
        mf = GetComponent<MeshFilter>();
        Vector3 meshSize = mf.mesh.bounds.size;
        transform.localScale = new Vector3(1/meshSize.x, 1/meshSize.y, 1/meshSize.z);
        transform.Translate(-mf.mesh.bounds.center * 1/meshSize.x);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
