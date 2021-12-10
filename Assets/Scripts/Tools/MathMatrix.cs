using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*1.矩阵常用  
 *        |m00 m01 m02 m03| 
 *        |m10 m11 m12 m13|      
 *        |m20 m21 m22 m23|       
 *        |m30 m31 m32 m33|       
 * 
 *2.缩放矩阵
 *        | Sx  0   0   0 |
 *        | 0   Sy  0   0 | 
 *        | 0   0   Sz  0 |
 *        | 0   0   0   1 |  
 *        
 *3.旋转矩阵
 *        | 1   0     0    0 |  | cosA  0  sinA  0 |  | cosA -sinA  0  0 |
 *        | 0  cosA -sinA  0 |  |  0    1   0    0 |  | sinA  cosA  0  0 |
 *        | 0  sinA  cosA  0 |  |-sinA  0  cosA  0 |  |  0     0    1  0 |
 *        | 0   0     0    1 |  |  0    0   0    1 |  |  0     0    0  1 |
 *   
 *4.平移矩阵
 *        | 1  0  0  Tx |
 *        | 0  1  0  Ty | 
 *        | 0  0  1  Tz |
 *        | 0  0  0   1 | 
 *5.复合变换 P' = Mt * Mr * Ms * P (缩放 -> 旋转 ->平移，阅读顺序从右到左)
 *6.Unity旋转顺序ZXY（Unity进行一次旋转时并没有旋转坐标系，实际矩阵相乘时顺序为Mrz * Mrx * Mry，不同旋转顺序得到的结果可能也不一样）
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

    public static Vector3 MultiplyPoint(Matrix4x4 mat, Vector3 v)
    {
        Vector3 result;
        result.x = mat.m00 * v.x + mat.m01 * v.y + mat.m02 * v.z + mat.m03;
        result.y = mat.m10 * v.x + mat.m11 * v.y + mat.m12 * v.z + mat.m13;
        result.z = mat.m20 * v.x + mat.m21 * v.y + mat.m22 * v.z + mat.m23;
        float num = mat.m30 * v.x + mat.m31 * v.y + mat.m32 * v.z + mat.m33;
        num = 1 / num;
        result.x *= num;
        result.y *= num;
        result.z *= num;
        return result;
    }

    public Vector3 MultiplyPoint3x4(Matrix4x4 mat, Vector3 v)
    {
        Vector3 result;
        result.x = (mat.m00 * v.x + mat.m01 * v.y + mat.m02 * v.z) + mat.m03;
        result.y = (mat.m10 * v.x + mat.m11 * v.y + mat.m12 * v.z) + mat.m13;
        result.z = (mat.m20 * v.x + mat.m21 * v.y + mat.m22 * v.z) + mat.m23;
        return result;
    }

    /// <summary>
    /// 世界到本地矩阵
    /// </summary>
    public static Matrix4x4 LocalToWorld(Transform transform)
    {
        ////变换顺序:缩放->旋转->平移  矩阵相乘顺序T*R*S
        //var tM = new Matrix4x4();
        //tM.SetTRS(transform.position, Quaternion.identity, Vector3.one);
        //var rM = new Matrix4x4();
        //rM.SetTRS(Vector3.zero, transform.rotation, Vector3.one);
        //var sM = new Matrix4x4();
        //sM.SetTRS(Vector3.zero, Quaternion.identity, transform.localScale);
        //return tM * rM * sM;

        var matrix = new Matrix4x4();
        matrix.SetTRS(transform.position, transform.rotation, transform.localScale);
        return matrix;
    }

    /// <summary>
    /// 本地到世界
    /// </summary>
    public static Matrix4x4 WorldToLocal(Transform transform)
    {
        return LocalToWorld(transform).inverse;
    }


    /// <summary>
    /// Float数组转化4X4矩阵
    /// </summary>
    /// <param name="floatArray">数组</param>
    /// <returns></returns>
    public static Matrix4x4 FloatToMatrix4X4(float[] floatArray)
    {
        return new Matrix4x4(
            new Vector4(floatArray[0], floatArray[1], floatArray[2], floatArray[3]),    //Column0
            new Vector4(floatArray[4], floatArray[5], floatArray[6], floatArray[7]),    //Column1
            new Vector4(floatArray[8], floatArray[9], floatArray[10], floatArray[11]),  //Column2
            new Vector4(floatArray[12], floatArray[13], floatArray[14], floatArray[15]) //Column3
        );
    }

    /// <summary>
    /// 4X4矩阵转化Float数组
    /// </summary>
    /// <param name="m">矩阵</param>
    /// <returns></returns>
    public static float[] Matrix4x4ToFloat(Matrix4x4 m)
    {
        float[] floatArray = new float[16];
        for (int i = 0; i < 16; i++)
        {
            floatArray[i] = m[i];
        }
        return floatArray;
    }
}
