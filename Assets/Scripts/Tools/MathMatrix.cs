using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathMatrix
{
    /// <summary>
    /// 转置
    /// </summary>
    public static Matrix4x4 MatrixTranspose(Matrix4x4 oriMatrix)
    {
        Vector4 row0 = oriMatrix.GetRow(0);
        Vector4 row1 = oriMatrix.GetRow(1);
        Vector4 row2 = oriMatrix.GetRow(2);
        Vector4 row3 = oriMatrix.GetRow(3);

        var marix = new Matrix4x4();
        marix.SetColumn(0, row0);
        marix.SetColumn(1, row1);
        marix.SetColumn(2, row2);
        marix.SetColumn(3, row3);
        return marix;
    }
}
