using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class Vector3Utils  {

    /// <summary>
    /// 设置各分量(Unity已包含)
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public static Vector3 Set(this Vector3 origin,float x, float y, float z)
    {
        origin.x = x;
        origin.y = y;
        origin.z = z;

        return origin;
    }

    /// <summary>
    /// 设置各分量统一大小
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="scalar"></param>
    /// <returns></returns>
    public static Vector3 SetScalar(this Vector3 origin ,float scalar)
    {
        origin.x = scalar;
        origin.y = scalar;
        origin.z = scalar;

        return origin;
    }

    /// <summary>
    /// 设置X分量大小
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector3 SetX(this Vector3 origin, float x)
    {
        origin.x = x;

        return origin;
    }
}
