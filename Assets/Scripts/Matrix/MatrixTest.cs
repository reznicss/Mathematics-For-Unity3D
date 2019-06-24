using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixTest : MonoBehaviour
{
    public Matrix4x4 matrix;

    private void Awake()
    {
        matrix = transform.localToWorldMatrix;
    }

    void Start()
    {
        //Debug.Log("矩阵:\n" + matrix);
        //Debug.Log("单位矩阵:\n" + Matrix4x4.identity);
        //Debug.Log("转置矩阵(沿主对角线翻转):\n" + matrix.transpose);
        //Debug.Log("逆矩阵:\n" + matrix.inverse);

        //CreateMesh(gameObject, new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 1, 0), new Vector3(1, 1, 0) }, new int[] { 0, 1, 2 }, Resources.Load("Materials/DefaultMat") as Material);

        //世界坐标点
        Vector3 worldPos = new Vector3(4f, 5f, 6f);

        var targetTransform = transform;
        //转换为本地坐标
        Matrix4x4 worldToLocal = targetTransform.worldToLocalMatrix;
        Debug.Log("转换本地坐标(矩阵,受Scale影响):" + worldToLocal.MultiplyPoint(worldPos));
        Matrix4x4 oriMatrix = Matrix4x4.identity;
        oriMatrix.SetTRS(worldPos, targetTransform.rotation, targetTransform.localScale);
        oriMatrix *= worldToLocal;
        var posColumn = oriMatrix.GetColumn(3);
        Debug.Log("转换本地坐标(本地矩阵):" + new Vector3(posColumn[0], posColumn[1], posColumn[2]));

        /** 三者区别：
         * 1.Point以targetTransform为起点做变换计算，Vector以世界坐标系零点为起点做变换计算。
         * 2.在转换过程中，point和vector均受物体的缩放影响（等比例改变），Direction以世界坐标系零点为起点，不受位置、缩放的影响
         * */
        Debug.Log("转换本地坐标(InverseTransformPoint，将位置从世界坐标转换为本地坐标，受缩放影响):" + targetTransform.InverseTransformPoint(worldPos));
        Debug.Log("转换本地坐标(InverseTransformVector，将坐标点从世界坐标转换为本地坐标，不受位置影响但受缩放影响):" + targetTransform.InverseTransformVector(worldPos));
        Debug.Log("转换本地坐标(InverseTransformDirection，不受位置、缩放的影响):" + targetTransform.InverseTransformDirection(worldPos));
    }

    void CreateMesh(GameObject go, Vector3[] vertices, int[] triangles, Material mat)
    {
        MeshFilter mf = go.AddComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mf.mesh = mesh;

        MeshRenderer mr = go.AddComponent<MeshRenderer>();
        mr.material = mat;
    }


}
