using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalizeV2 : MonoBehaviour
{
    [Header("Eje de normalizacion")]
    [Tooltip("Selecciona 1 dimension para escalar")]
    public bool x;
    public bool y, z;
    // Start is called before the first frame update
    void Awake()
    {
        MeshFilter mf = GetComponent<MeshFilter>();
        Vector3 meshSize = mf.mesh.bounds.size;

        float scale = 1;
        if (x) scale = 1/meshSize.x;
        else if (y) scale = 1/meshSize.y;
        else if (z) scale = 1/meshSize.z;

        transform.localScale = new Vector3(scale, scale, scale);
        transform.Translate(-mf.mesh.bounds.center * scale);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
