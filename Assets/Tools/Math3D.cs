using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Math3D  {

    public static float V3Magnitude(Vector3 a)
    {
        return Mathf.Sqrt(a.x * a.x + a.y * a.y + a.z * a.z);
    }

    public static float V3SqrMagnitude(Vector3 a)
    {
        return a.x * a.x + a.y * a.y + a.z * a.z;
    }

    public static float V3Dot(Vector3 a,Vector3 b)
    {
        return a.x * b.x + a.y * b.y + a.z * b.z;
    }

    public static float V3Angle(Vector3 a, Vector3 b)
    {
        var f1 = (a.x * b.x + a.y * b.y + a.z * b.z);
        var f2 = Mathf.Sqrt(a.x * a.x + a.y * a.y + a.z * a.z) * Mathf.Sqrt(b.x * b.x + b.y * b.y + b.z * b.z);
        //Mathf.Deg2Rad 表示从角度到弧度转变的常量值, 其值为(2 * Mathf.PI)/ 360
        //Mathf.Rad2Deg 表示从弧度到角度转变的常量值, 其值为360/ (2 * Mathf.PI)
        return Mathf.Rad2Deg * Mathf.Acos(f1 / f2) ;
    }
}
