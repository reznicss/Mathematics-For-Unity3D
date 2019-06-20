using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 1.‖P + Q‖ ≤ ‖P‖+ ‖Q‖     几何意义，三角形的两边之和大于第三边
 * 2.‖P· Q‖ ≤ ‖P‖‖Q‖       Cauchy-Schwarz不等式
 * 3. P·Q = ‖P‖‖Q‖cosα     根据余弦定理推得(‖P - Q‖² = ‖P‖²+ ‖Q‖² - 2||P|| ||Q|| cosα)
 * */

public class Math3D
{

    /// <summary>
    /// 模
    /// </summary>
    public static float V3Magnitude(Vector3 a)
    {
        return Mathf.Sqrt(a.x * a.x + a.y * a.y + a.z * a.z);
    }

    /// <summary>
    /// 模平方
    /// </summary>
    public static float V3SqrMagnitude(Vector3 a)
    {
        return a.x * a.x + a.y * a.y + a.z * a.z;
    }

    /// <summary>
    /// 点乘
    /// </summary>
    public static float V3Dot(Vector3 a, Vector3 b)
    {
        //大于0同向，小鱼0反向，等于0垂直
        return a.x * b.x + a.y * b.y + a.z * b.z;
    }

    /// <summary>
    /// 夹角(0到180)
    /// </summary>
    public static float V3Angle(Vector3 a, Vector3 b)
    {

        //Mathf.Deg2Rad 表示从角度到弧度转变的常量值, 其值为(2 * Mathf.PI) / 360
        //Mathf.Rad2Deg 表示从弧度到角度转变的常量值, 其值为360 / (2 * Mathf.PI)
        var f1 = (a.x * b.x + a.y * b.y + a.z * b.z);
        var f2 = Mathf.Sqrt(a.x * a.x + a.y * a.y + a.z * a.z) * Mathf.Sqrt(b.x * b.x + b.y * b.y + b.z * b.z);
        return Mathf.Rad2Deg * Mathf.Acos(f1 / f2);
    }

    /// <summary>
    /// 夹角(0到180 带正负)
    /// </summary>
    public static float V3SignedAngle(Vector3 a, Vector3 b, Vector3 axis)
    {
        float angle = Vector3.Angle(a, b);
        angle *= Mathf.Sign(Vector3.Cross(a, b).y);
        return angle;
    }

    public static float V3SignedAngle(Vector3 a, Vector3 b)
    {
        float angle = Vector3.Angle(a, b);
        angle *= Mathf.Sign(Vector3.Cross(a, b).y);
        return angle;
    }


    /// <summary>
    /// 夹角(0到360,axis为(0, 1, 0)时顺时针,(0,-1,0)逆时针)
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="axis"></param>
    /// <returns></returns>
    public static float V3SignedAngleBetween(Vector3 a, Vector3 b, Vector3 axis)
    {
        float angle = Vector3.Angle(a, b);
        float sign = Mathf.Sign(Vector3.Dot(axis, Vector3.Cross(a, b)));
        float signed_angle = angle * sign;
        return (signed_angle <= 0) ? 360 + signed_angle : signed_angle;
    }


}
