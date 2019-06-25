using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MathTransform
{
    /// <summary>
    /// 世界到本地矩阵
    /// </summary>
    public static Matrix4x4 LocalToWorld(Transform transform)
    {
        //T*R*S
        var tM = new Matrix4x4();
        tM.SetTRS(transform.position, Quaternion.identity, Vector3.one);

        var rM = new Matrix4x4();
        rM.SetTRS(Vector3.zero, transform.rotation, Vector3.one);

        var sM = new Matrix4x4();
        sM.SetTRS(Vector3.zero, Quaternion.identity, transform.localScale);
        return tM * rM * sM;
    }

    /// <summary>
    /// 本地到世界
    /// </summary>
    public static Matrix4x4 WorldToLocal(Transform transform)
    {
        return LocalToWorld(transform).inverse;
    }

    /// <summary>
    /// 世界坐标转屏幕坐标(MVP)
    /// </summary>
    public static Vector3 WorldToScreenPoint(Camera camera, Vector3 objPos)
    {
        Matrix4x4 matrix = camera.projectionMatrix * camera.worldToCameraMatrix;
        Vector3 screenPos = matrix.MultiplyPoint(objPos);
        // (-1, 1)'s clip => (0 ,1)'s viewport 
        screenPos = new Vector3(screenPos.x + 1f, screenPos.y + 1f, screenPos.z + 1f) / 2f;
        // viewport => screen
        screenPos = new Vector3(screenPos.x * Screen.width, screenPos.y * Screen.height, objPos.z - camera.transform.position.z);
        return screenPos;
    }


}
