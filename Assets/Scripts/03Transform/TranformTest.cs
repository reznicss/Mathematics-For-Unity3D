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
        Debug.Log("世界坐标:" + MathTransform.LocalToWorldPos(transform)) ;
        Debug.Log("转换世界坐标(TransformPoint):" + transform.TransformPoint(localPos) + "____" + MathTransform.TransformPoint(transform, localPos));
        Debug.Log("转换世界坐标(TransformVector):" + transform.TransformVector(localPos) + "____" + MathTransform.TransformVector(transform, localPos));
        Debug.Log("转换世界坐标(TransformDirection):" + transform.TransformDirection(localPos) + "____" + MathTransform.TransformDirection(transform, localPos));

        Debug.Log("转换本地坐标(InverseTransformPoint):" + transform.InverseTransformPoint(worldPos) + "____" + MathTransform.InverseTransformPoint(transform,worldPos));
        Debug.Log("转换本地坐标(InverseTransformVector):" + transform.InverseTransformVector(worldPos) + "____" + MathTransform.InverseTransformVector(transform,worldPos));
        Debug.Log("转换本地坐标(InverseTransformDirection):" + transform.InverseTransformDirection(worldPos) + "____" + MathTransform.InverseTransformDirection(transform,worldPos));

        var pos = new Vector3(1, 2, 3);
        Debug.Log("世界坐标转视口坐标:" + mainCam.WorldToViewportPoint(pos) + "____" + MathTransform.WorldToViewportPoint(mainCam,pos));
        Debug.Log("世界坐标转屏幕坐标点:" + mainCam.WorldToScreenPoint(pos) + "____" + MathTransform.WorldToScreenPoint(mainCam, pos));
    }

    void OnDrawGizmosSelected()
    {
        mainCam = GetComponent<Camera>();
        Vector3 p = mainCam.ViewportToWorldPoint(new Vector3(1, 1, mainCam.nearClipPlane));
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(p, 0.1F);
    }
}
