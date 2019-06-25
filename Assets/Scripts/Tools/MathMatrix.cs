using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*矩阵常用  
 *        |m00 m01 m02 m03| 
 *        |m10 m11 m12 m13|      
 *        |m20 m21 m22 m23|       
 *        |m30 m31 m32 m33|       
 * 
 *缩放矩阵| Sx  0   0   0 |
 *        | 0   Sy  0   0 | 
 *        | 0   0   Sz  0 |
 *        | 0   0   0   1 |  
 *        
 *平移矩阵| 1   0     0    0 |  | cosA  0  sinA  0 |  | cosA -sinA  0  0 |
 *        | 0  cosA -sinA  0 |  |  0    1   0    0 |  | sinA  cosA  0  0 |
 *        | 0  sinA  cosA  0 |  |-sinA  0  cosA  0 |  |  0     0    1  0 |
 *        | 0   0     0    1 |  |  0    0   0    1 |  |  0     0    0  1 |
 *   
 *平移矩阵| 1  0  0  Tx |
 *        | 0  1  0  Ty | 
 *        | 0  0  1  Tz |
 *        | 0  0  0   1 | 
 */
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

    public static Vector3 GetPostion(Matrix4x4 matrix)
    {
        var x = matrix.m03;
        var y = matrix.m13;
        var z = matrix.m23;
        return new Vector3(x, y, z);
    }

    public static Quaternion GetRotation(Matrix4x4 matrix)
    {
        float qw = Mathf.Sqrt(1f + matrix.m00 + matrix.m11 + matrix.m22) / 2;
        float w = 4 * qw;
        float qx = (matrix.m21 - matrix.m12) / w;
        float qy = (matrix.m02 - matrix.m20) / w;
        float qz = (matrix.m10 - matrix.m01) / w;
        return new Quaternion(qx, qy, qz, qw);
    }

    public static Vector3 GetScale(Matrix4x4 matrix)
    {
        var x = Mathf.Sqrt(matrix.m00 * matrix.m00 + matrix.m01 * matrix.m01 + matrix.m02 * matrix.m02);
        var y = Mathf.Sqrt(matrix.m10 * matrix.m10 + matrix.m11 * matrix.m11 + matrix.m12 * matrix.m12);
        var z = Mathf.Sqrt(matrix.m20 * matrix.m20 + matrix.m21 * matrix.m21 + matrix.m22 * matrix.m22);
        return new Vector3(x, y, z);
    }

}
