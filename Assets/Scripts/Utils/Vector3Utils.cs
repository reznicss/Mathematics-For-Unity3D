using System;
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
    public static Vector3 Set(this ref Vector3 origin,float x, float y, float z)
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
    public static Vector3 SetScalar(this ref Vector3 origin,float scalar)
    {

        origin.x = scalar;
        origin.y = scalar;
        origin.z = scalar;

        return origin;

    }

    /// <summary>
    /// 设置X分量
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector3 SetX(this ref Vector3 origin, float x)
    {

        origin.x = x;

        return origin;

    }

    /// <summary>
    /// 设置Y分量
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static Vector3 SetY(this ref Vector3 origin, float y)
    {

        origin.y = y;

        return origin;

    }

    /// <summary>
    /// 设置Z分量
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public static Vector3 SetZ(this ref Vector3 origin, float z)
    {
        origin.z = z;

        return origin;
    }

    /// <summary>
    /// 根据下标设置向量分量
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="index">X-0,Y-1,Z-2</param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Vector3 SetComponent(this ref Vector3 origin, int index, float value)
    {
        
        switch (index)
        {
            case 0: origin.x = value; break;
            case 1: origin.y = value; break;
            case 2: origin.z = value; break;
            default: throw new Exception($"{index}为异常下标");
        }

        return origin;

    }

    /// <summary>
    /// 根据下标获取向量分量
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="index">X-0,Y-1,Z-2</param>
    /// <returns></returns>
    public static float GetComponent(this Vector3 origin, int index)
    {

        switch (index)
        {
            case 0: return origin.x;
            case 1: return origin.y;
            case 2: return origin.z;
            default: throw new Exception($"{index}为异常下标");
        }

    }

    /// <summary>
    /// 克隆向量
    /// </summary>
    /// <param name="origin"></param>
    /// <returns></returns>
    public static Vector3 Clone(this Vector3 origin)
    {

        return new Vector3(origin.x,origin.y,origin.z);

    }

    /// <summary>
    /// 复制目标向量
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="v"></param>
    /// <returns></returns>
    public static Vector3 Copy (this ref Vector3 origin,Vector3 v)
    {

        origin.x = v.x;
        origin.y = v.y;
        origin.z = v.z;

        return origin;

    }

    /// <summary>
    /// 向量加法
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="v"></param>
    /// <param name="w"></param>
    /// <returns></returns>
    public static Vector3 Add(this ref Vector3 origin,Vector3 v, Vector3 w = new Vector3())
    {

        if (w != Vector3.zero)
        {
            return origin.AddVectors(v, w);
        }

        origin.x += v.x;
        origin.y += v.y;
        origin.z += v.z;

        return origin;
    }


    /// <summary>
    /// 赋值另外两个向量相加值
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Vector3 AddVectors(this ref Vector3 origin, Vector3 a, Vector3 b)
    {
        origin.x = a.x + b.x;
        origin.y = a.y + b.y;
        origin.z = a.z + b.z;

        return origin;
    }

    /// <summary>
    /// 各分量同时增加相同变量
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="s"></param>
    /// <returns></returns>
    public static Vector3 AddScalar(this ref Vector3 origin,float s)
    {

        origin.x += s;
        origin.y += s;
        origin.z += s;

        return origin;

    }

    /// <summary>
    /// 赋值某向量的增量倍增
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="v"></param>
    /// <param name="s"></param>
    /// <returns></returns>
    public static Vector3 AddScaledVector(this ref Vector3 origin, Vector3 v, float s )
    {

        origin.x += v.x * s;
        origin.y += v.y * s;
        origin.z += v.z * s;

        return origin;

    }

    /// <summary>
    /// 向量减法
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="v"></param>
    /// <param name="w"></param>
    /// <returns></returns>
    public static Vector3 Sub(this ref Vector3 origin, Vector3 v, Vector3 w = new Vector3())
    {

        if (w != Vector3.zero)
        {

            return origin.SubVectors(v, w);

        }

        origin.x -= v.x;
        origin.y -= v.y;
        origin.z -= v.z;

        return origin;

    }

    /// <summary>
    /// 赋值另外两个向量相减值
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Vector3 SubVectors (this ref Vector3 origin, Vector3 a, Vector3 b)
    {

        origin.x = a.x - b.x;
        origin.y = a.y - b.y;
        origin.z = a.z - b.z;

        return origin;

    }

    /// <summary>
    /// 各分量同时减少相同变量
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="s"></param>
    /// <returns></returns>
    public static Vector3 SubScalar(this ref Vector3 origin, float s)
    {

        origin.x -= s;
        origin.y -= s;
        origin.z -= s;

        return origin;

    }

    /// <summary>
    /// 向量乘法
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="v"></param>
    /// <param name="w"></param>
    /// <returns></returns>
    public static Vector3 Multiply(this ref Vector3 origin, Vector3 v, Vector3 w = new Vector3())
    {

        if (w != Vector3.zero)
        {

            return origin.MultiplyVectors(v, w);

        }

        origin.x *= v.x;
        origin.y *= v.y;
        origin.z *= v.z;

        return origin;

    }

    /// <summary>
    /// 各分量同时乘以相同变量
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="scalar"></param>
    /// <returns></returns>
    public static Vector3 MultiplyScalar(this ref Vector3 origin, float scalar )
    {

        origin.x *= scalar;
        origin.y *= scalar;
        origin.z *= scalar;

        return origin;

    }

    /// <summary>
    /// 赋值另外两个向量相乘
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Vector3 MultiplyVectors(this ref Vector3 origin, Vector3 a,Vector3 b)
    {

        origin.x = a.x * b.x;
        origin.y = a.y * b.y;
        origin.z = a.z * b.z;

        return origin;

    }




}
