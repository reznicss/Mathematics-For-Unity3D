using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TranformTest : MonoBehaviour {

    public Matrix4x4 matrix;
    Camera mainCam;


    private void Awake()
    {
        matrix = transform.localToWorldMatrix;
        mainCam = GetComponent<Camera>();
    }

    void Start()
    {
        var worldPos = transform.position;
        var localPos = transform.localPosition;
        //Debug.Log("世界坐标:" + MathTransform.LocalToWorldPos(transform)) ;
        //Debug.Log("转换世界坐标(TransformPoint):" + transform.TransformPoint(localPos) + "____" + MathTransform.TransformPoint(transform, localPos));
        //Debug.Log("转换世界坐标(TransformVector):" + transform.TransformVector(localPos) + "____" + MathTransform.TransformVector(transform, localPos));
        //Debug.Log("转换世界坐标(TransformDirection):" + transform.TransformDirection(localPos) + "____" + MathTransform.TransformDirection(transform, localPos));

        //Debug.Log("转换本地坐标(InverseTransformPoint):" + transform.InverseTransformPoint(worldPos) + "____" + MathTransform.InverseTransformPoint(transform,worldPos));
        //Debug.Log("转换本地坐标(InverseTransformVector):" + transform.InverseTransformVector(worldPos) + "____" + MathTransform.InverseTransformVector(transform,worldPos));
        //Debug.Log("转换本地坐标(InverseTransformDirection):" + transform.InverseTransformDirection(worldPos) + "____" + MathTransform.InverseTransformDirection(transform,worldPos));

        var pos = new Vector3(1f, 2f, 3f);
        //Debug.Log("世界坐标转视口坐标:" + mainCam.WorldToViewportPoint(pos) + "____" + MathTransform.WorldToViewportPoint(mainCam,pos));
        //Debug.Log("世界坐标转屏幕坐标点:" + mainCam.WorldToScreenPoint(pos) + "____" + MathTransform.WorldToScreenPoint(mainCam, pos));

        var rotaion =  Quaternion.LookRotation(pos) * Quaternion.Euler(90f, 0f, 0f);
        Mesh mesh = new Mesh();
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix.SetTRS(Vector3.zero, rotaion, Vector3.one);
        mesh.vertices = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 1, 0), new Vector3(1, 1, 0), new Vector3(1, 0, 0) };
        mesh.triangles = new int[] { 0, 1, 2, 2, 3, 0 };
        //GameObject go = new GameObject();
        //go.AddComponent<MeshFilter>().mesh = mesh;
        //go.AddComponent<MeshRenderer>();
        Debug.DrawLine(Vector3.zero, pos, Color.black, 1000f);

        Mesh mesh2 = new Mesh();
        Matrix4x4 matrix2 = Matrix4x4.identity;
        mesh2.vertices = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 1, 0), new Vector3(1, 1, 0), new Vector3(1, 0, 0) };
        mesh2.triangles = new int[] { 0, 1, 2, 2, 3, 0 };
        //GameObject go2 = new GameObject();
        //go2.AddComponent<MeshFilter>().mesh = mesh2;
        //go2.AddComponent<MeshRenderer>();

        CombineInstance[] combine = new CombineInstance[2];
        combine[0].mesh = mesh;
        combine[0].transform = matrix;

        combine[1].mesh = mesh2;
        combine[1].transform = matrix2;

        GameObject go3 = new GameObject();
        Mesh meshcombine = new Mesh();
        meshcombine.CombineMeshes(combine);
        go3.AddComponent<MeshFilter>().mesh = meshcombine;
        go3.AddComponent<MeshRenderer>();


    }

    //void OnDrawGizmosSelected()
    //{
    //    mainCam = GetComponent<Camera>();
    //    Vector3 p = mainCam.ViewportToWorldPoint(new Vector3(1, 1, mainCam.nearClipPlane));
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawSphere(p, 0.1F);
    //}
}
