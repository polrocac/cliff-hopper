using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CoordManager
{
    public static Vector2 toCHCoords(Vector3 pos)
    {
        float x = pos.z - pos.x;
        float xz = (pos.x + pos.z) * Mathf.Cos(Mathf.PI / 4);
        float y = xz * -Mathf.Cos(Mathf.Deg2Rad * 60) + pos.y * Mathf.Cos(Mathf.Deg2Rad * 30);
        return new Vector2(x, y);
    }
}
