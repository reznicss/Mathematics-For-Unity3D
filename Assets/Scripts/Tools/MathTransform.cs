using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathTransform
{
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
    /// 世界坐标转屏幕坐标
    /// </summary>
    public static Vector3 WorldToScreenPoint(Camera camera, Vector3 objPos)
    {
        Matrix4x4 v = camera.worldToCameraMatrix;
        Matrix4x4 p = camera.projectionMatrix;
        Matrix4x4 vp = p * v;
        //获取投影坐标
        Vector3 screenPos = vp.MultiplyPoint(objPos);
        // (-1, 1)'s clip => (0 ,1)'s viewport 
        screenPos = new Vector3(screenPos.x + 1f, screenPos.y + 1f, screenPos.z + 1f) / 2f;
        // viewport => screen
        screenPos = new Vector3(screenPos.x * Screen.width, screenPos.y * Screen.height, objPos.z - camera.transform.position.z);
        return screenPos;
    }
    #endregion

}
