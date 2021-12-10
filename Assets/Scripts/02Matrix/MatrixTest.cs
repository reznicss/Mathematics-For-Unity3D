using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixTest : MonoBehaviour
{
    public Matrix4x4 matrix;
    public Vector4 column0;
    public Vector4 column1;
    public Vector4 column2;
    public Vector4 column3;

    private void Awake()
    {
        matrix = transform.localToWorldMatrix;
        Debug.Log(matrix);
        column0 = matrix.GetColumn(0);
        column1 = matrix.GetColumn(1);
        column2 = matrix.GetColumn(2);
        column3 = matrix.GetColumn(3);
    }

    void Start()
    {
        //Debug.Log("矩阵(本地 -> 世界):\n" + transform.localToWorldMatrix + "---------\n" + MathMatrix.LocalToWorld(transform));
        //Debug.Log("矩阵(世界 -> 本地):\n" + transform.worldToLocalMatrix + "---------\n" + MathMatrix.WorldToLocal(transform));
        //Debug.Log("单位矩阵:\n" + Matrix4x4.identity);
        //Debug.Log("逆矩阵:\n" + matrix.inverse + "---------\n" + matrix * matrix.inverse);
        //Debug.Log("转置矩阵(沿主对角线翻转):\n" + matrix.transpose + "---------\n" + MathMatrix.MatrixTranspose(matrix));
        //CreateMesh(gameObject, new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 1, 0), new Vector3(1, 1, 0) }, new int[] { 0, 1, 2 }, Resources.Load("Materials/DefaultMat") as Material);
    }

    //private void OnGUI()
    //{
    //    var origin = Camera.main.ViewportToScreenPoint(new Vector3(0.25f, 0.1f, 0));
    //    var extent = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.2f, 0));

    //    GUI.DrawTexture(new Rect(origin.x, origin.y, extent.x, extent.y), Resources.Load("Textures/Red") as Texture2D);
    //}

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
