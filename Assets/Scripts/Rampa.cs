using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rampa : Platform
{
    public override void SetHeight(float h)
    {
        //terrainTransf.localScale = new Vector3(terrainTransf.localScale.x, (24 * h)/5, terrainTransf.localScale.z);
        //terrainTransf.Translate(0, (1 - h) / 2, 0);
    }
}
