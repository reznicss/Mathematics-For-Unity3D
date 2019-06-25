using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixTest : MonoBehaviour
{
    Camera mainCam;
    public Matrix4x4 matrix;

    private void Awake()
    {
        matrix = transform.localToWorldMatrix;
        mainCam = GetComponent<Camera>();
    }

    void Start()
    {
        Debug.Log("矩阵(本地 -> 世界):\n" + transform.localToWorldMatrix + "---------\n" + MathTransform.LocalToWorld(transform));
        Debug.Log("矩阵(世界 -> 本地):\n" + transform.worldToLocalMatrix + "---------\n" + MathTransform.WorldToLocal(transform));
        Debug.Log("单位矩阵:\n" + Matrix4x4.identity);
        Debug.Log("逆矩阵:\n" + matrix.inverse + "---------\n" + matrix * matrix.inverse);
        Debug.Log("转置矩阵(沿主对角线翻转):\n" + matrix.transpose + "---------\n" + MathMatrix.MatrixTranspose(matrix));
        //CreateMesh(gameObject, new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 1, 0), new Vector3(1, 1, 0) }, new int[] { 0, 1, 2 }, Resources.Load("Materials/DefaultMat") as Material);

        var pos = new Vector3(1, 1, 16);
        Debug.Log("世界坐标转屏幕坐标点:" + mainCam.WorldToScreenPoint(pos) + "____" + MathTransform.WorldToScreenPoint(mainCam, pos));
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

    void OnDrawGizmosSelected()
    {
        mainCam = GetComponent<Camera>();
        Vector3 p = mainCam.ViewportToWorldPoint(new Vector3(1, 1, mainCam.nearClipPlane));
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(p, 0.1F);
    }

}
