using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixTest : MonoBehaviour
{
    Camera camera;
    public Matrix4x4 matrix;

    private void Awake()
    {
        matrix = transform.localToWorldMatrix;
        camera = GetComponent<Camera>();
    }

    void Start()
    {
        Debug.Log("矩阵:\n" + matrix);
        Debug.Log("单位矩阵:\n" + Matrix4x4.identity);
        Debug.Log("逆矩阵:\n" + matrix.inverse + "---------\n" + matrix * matrix.inverse);
        Debug.Log("转置矩阵(沿主对角线翻转):\n" + matrix.transpose + "---------\n" + MathMatrix.MatrixTranspose(matrix));
        //CreateMesh(gameObject, new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 1, 0), new Vector3(1, 1, 0) }, new int[] { 0, 1, 2 }, Resources.Load("Materials/DefaultMat") as Material);


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
