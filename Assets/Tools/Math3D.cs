using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 1.‖P + Q‖ ≤ ‖P‖+ ‖Q‖     几何意义，三角形的两边之和大于第三边
 * 2.‖P· Q‖ ≤ ‖P‖‖Q‖       Cauchy-Schwarz不等式
 * 3. P·Q = ‖P‖‖Q‖cosα     根据余弦定理推得(‖P - Q‖² = ‖P‖²+ ‖Q‖² - 2||P|| ||Q|| cosα)
 * 4. P·P = ‖P‖²
 * 5. P×Q·P =  P×Q·Q = 0
 * 6.‖P×Q‖=‖P‖‖Q‖sinα (平行四边形)
 * 7. Area = 1/2 *‖(V2 - V1)×(V3 - V1)‖ (由V1,V2,V3三个顶点组成的任意三角形面积
 * 8. P×Q = -(Q×P) 
 * 9. P×P = (0,0,0)
 * 10.(P×Q)·R = (R×P)·Q =(Q×R)·P
 * 11.P×(Q+R) = P×Q + P×R
 * 12.P×(Q×P) = P×Q×P = P²Q -(P·Q)P
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
        //P·Q = ‖P‖‖Q‖cosα
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
    public static float V3SignedAngleBetween(Vector3 a, Vector3 b, Vector3 axis)
    {
        float angle = Vector3.Angle(a, b);
        float sign = Mathf.Sign(Vector3.Dot(axis, Vector3.Cross(a, b)));
        float signed_angle = angle * sign;
        return (signed_angle <= 0) ? 360 + signed_angle : signed_angle;
    }

    /// <summary>
    /// 向量投影(向量a在向量b上投影)
    /// </summary>
    public static Vector3 V3Project(Vector3 a, Vector3 b)
    {
        // cosα  = a · b / (‖a‖‖b‖) = ‖projB‖/‖a‖
        // => ‖projB‖ =  a · b / (‖a‖‖b‖) * ‖a‖
        // => projB = a · b / (‖a‖‖b‖) * ‖a‖ * ( b /‖b‖) 
        return b * Vector3.Dot(a, b) / Vector3.Dot(b, b);
    }

    /// <summary>
    /// 向量投影(a在法线为b的平面上的投影向量,即向量a相对于向量b的垂直向量)
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Vector3 V3ProjectOnPlane(Vector3 a, Vector3 b)
    {
        return a - b * Vector3.Dot(a, b) / Vector3.Dot(b, b);
    }

    /// <summary>
    /// 按照轨道朝目标的运动方向
    /// </summary>
    /// <param name="selfPos">当前位置</param>
    /// <param name="tarPos">目标对象位置</param>
    /// <param name="railDirection">轨道位置</param>
    /// <returns>运动方向</returns>
    public static Vector3 V3MoveDirectionByProject(Vector3 selfPos, Vector3 tarPos, Vector3 railDirection)
    {
        var heading = tarPos - selfPos;
        var force = Vector3.Project(heading, railDirection);
        return force;
    }

    /// <summary>
    /// 叉乘
    /// </summary>
    public static Vector3 V3Cross(Vector3 a, Vector3 b)
    {
        /**
         * | i   j   k   |     |0   -Pz  Py| |Qx| 
         * | Px  Py  Pz  |     |Pz   0  -Px| |Qy|
         * | Qx  Qy  Qz  |     |-Py  Px  0 | |Qz|
         * = i(PyQz - PzQy) - j(PxQz - PzQx) + k(PxQy - PyQx)
         * */
        var c = new Vector3(a.y * b.z - a.z * b.y, a.z * b.x - a.x * b.z, a.x * b.y - a.y * b.x);
        //遵守左手定律(Unity为左手坐标系)
        if (c.y > 0)
        {
            // b在a的顺时针
        }
        else if (c.y == 0)
        {
            // b和a平行
        }
        else
        {
            // b在a的逆时针
        }
        return c;
    }

}
