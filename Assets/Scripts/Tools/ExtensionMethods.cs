using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods {
    public static Vector2 ToXZ(this Vector3 v3)
    {
        return new Vector2(v3.x, v3.z);
    }

    public static Matrix4x4 GetRotMatrix(this Quaternion q)
    {
        Matrix4x4 matrix = new Matrix4x4();
        matrix.SetTRS(Vector3.zero, q, Vector3.one);
        return matrix;
    }

}
