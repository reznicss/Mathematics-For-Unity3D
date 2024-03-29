﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*四元数  
 *1.q = <w,x,y,z> = w +xi + yj + zk 
 *2.i² + j² + k² = -1
 *3.不满足交换律,乘法时必须保证顺序
 *  ij = -ji = k 
 *  jk = -kj = i
 *  ki = -ik = j
 *  
 */
public class MathTransform
{
    /// <summary>
    /// 本地坐标转换为世界坐标(以transform为起点，受缩放影响)
    /// </summary>
    public static Vector3 TransformPoint(Transform transform, Vector3 point)
    {
        Matrix4x4 localToWorld = new Matrix4x4();
        localToWorld.SetTRS(transform.position, transform.rotation, transform.localScale);
        return localToWorld.MultiplyPoint(point);
    }

    /// <summary>
    /// 本地坐标转换为世界坐标(Vector3.zero为起点，受缩放影响)
    /// </summary>
    public static Vector3 TransformVector(Transform transform, Vector3 point)
    {
        Matrix4x4 localToWorld = new Matrix4x4();
        localToWorld.SetTRS(Vector3.zero, transform.rotation, transform.localScale);
        return localToWorld.MultiplyPoint(point);
    }

    /// <summary>
    /// 本地坐标转换为世界坐标(Vector3.zero为起点，不受位置、缩放影响)
    /// </summary>
    public static Vector3 TransformDirection(Transform transform, Vector3 point)
    {
        Matrix4x4 localToWorld = new Matrix4x4();
        localToWorld.SetTRS(Vector3.zero, transform.rotation, Vector3.one);
        return localToWorld.MultiplyPoint(point);
    }

    /// <summary>
    /// 世界坐标转换为本地坐标(以transform为起点，受缩放影响)
    /// </summary>
    public static Vector3 InverseTransformPoint(Transform transform, Vector3 point)
    {
        Matrix4x4 localToWorld = new Matrix4x4();
        localToWorld.SetTRS(transform.position, transform.rotation, transform.localScale);
        return localToWorld.inverse.MultiplyPoint(point);
    }

    /// <summary>
    /// 世界坐标转换为本地坐标(Vector3.zero为起点，受缩放影响)
    /// </summary>
    public static Vector3 InverseTransformVector(Transform transform, Vector3 point)
    {
        Matrix4x4 localToWorld = new Matrix4x4();
        localToWorld.SetTRS(Vector3.zero, transform.rotation, transform.localScale);
        return localToWorld.inverse.MultiplyPoint(point);
    }

    /// <summary>
    /// 世界坐标转换为本地坐标(Vector3.zero为起点，不受位置、缩放影响)
    /// </summary>
    public static Vector3 InverseTransformDirection(Transform transform, Vector3 point)
    {
        Matrix4x4 localToWorld = new Matrix4x4();
        localToWorld.SetTRS(Vector3.zero, transform.rotation, Vector3.one);
        return localToWorld.inverse.MultiplyPoint(point);
    }

    /// <summary>
    /// 本地坐标转世界坐标
    /// </summary>
    public static Vector3 LocalToWorldPos(Transform transform)
    {
        if (transform.parent)
        {
            //理解世界坐标转换
            return transform.parent.localToWorldMatrix.MultiplyPoint(transform.localPosition);
        }
        else
        {
            return transform.position;
        }
        //理解有父物体情况下的逐层转换
        //    Vector3 pos = transform.localPosition;
        //    var tempT = transform;
        //    while (tempT.parent)
        //    {
        //        var m = new Matrix4x4();
        //        var parentT = tempT.parent;
        //        m.SetTRS(parentT.localPosition, parentT.localRotation, parentT.localScale);
        //        pos = m.MultiplyPoint(pos);
        //        tempT = tempT.parent;
        //    }
        //    return pos;
    }

    #region MVP推导
    /// <summary>
    /// 世界坐标转视口坐标(View Port)
    /// </summary>
    public static Vector3 WorldToViewportPoint(Camera camera, Vector3 objPos)
    {
        Vector3 pos = Vector3.zero;
        Matrix4x4 v = camera.worldToCameraMatrix;
        Matrix4x4 p = camera.projectionMatrix;
        Matrix4x4 vp =  p * v;
 
        //获取投影坐标
        pos = vp.MultiplyPoint(new Vector4(objPos.x,objPos.y,objPos.z,1));
        Debug.Log(pos);
        //Z坐标表示目标点据相机平面的垂直距离(即相机坐标系中Z值)
        var z = camera.transform.worldToLocalMatrix.MultiplyPoint(objPos).z;
        // (-1, 1)'s clip => (0 ,1)'s viewport 
        pos = new Vector3((pos.x + 1f) / 2, (pos.y + 1f) / 2, z);
        return pos;
    }

    /// <summary>
    /// 世界坐标转屏幕坐标(Screen Space)
    /// </summary>
    public static Vector3 WorldToScreenPoint(Camera camera, Vector3 objPos)
    {
        Vector3 forward = camera.transform.TransformDirection(Vector3.forward);
        Vector3 toOther = objPos - camera.transform.position;
        Vector3 pos = Vector3.zero;
        //必须在摄像机前方
        if (Vector3.Dot(forward, toOther) > 0)
        {
            Matrix4x4 v = camera.worldToCameraMatrix;
            Matrix4x4 p = camera.projectionMatrix;
            Matrix4x4 vp = p * v;
            //获取投影坐标
            pos = vp.MultiplyPoint(objPos);
            var z = camera.transform.worldToLocalMatrix.MultiplyPoint(objPos).z;
            pos = new Vector3((pos.x + 1f) / 2, (pos.y + 1f) / 2, z);
            //视口 => 屏幕
            pos = new Vector3(pos.x * Screen.width, pos.y * Screen.height, z);
        }
        return pos;
    }
    #endregion

}
