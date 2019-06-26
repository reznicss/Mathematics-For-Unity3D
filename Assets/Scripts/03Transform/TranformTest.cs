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
        Vector3 worldPos = new Vector3(4f, 5f, 6f);

        /*三者区别：
         * 2.在转换过程中，point和vector均受物体的缩放影响（等比例改变），Direction以世界坐标系零点为起点，不受位置、缩放的影响
         */
        Debug.Log("世界坐标:" + MathTransform.LocalToWorldPos(transform)) ;
        Debug.Log("转换本地坐标(InverseTransformPoint，将位置从世界坐标转换为本地坐标，受缩放影响):" + transform.InverseTransformPoint(worldPos) + "____" + MathTransform.InverseTransformPoint(transform,worldPos));
        Debug.Log("转换本地坐标(InverseTransformVector，将坐标点从世界坐标转换为本地坐标，不受位置影响但受缩放影响):" + transform.InverseTransformVector(worldPos) + "____" + MathTransform.InverseTransformVector(transform,worldPos));
        Debug.Log("转换本地坐标(InverseTransformDirection，不受位置、缩放的影响):" + transform.InverseTransformDirection(worldPos) + "____" + MathTransform.InverseTransformDirection(transform,worldPos));

        Debug.Log("世界坐标转屏幕坐标点:" + mainCam.WorldToScreenPoint(worldPos) + "____" + MathTransform.WorldToScreenPoint(mainCam, worldPos));
    }

    void OnDrawGizmosSelected()
    {
        mainCam = GetComponent<Camera>();
        Vector3 p = mainCam.ViewportToWorldPoint(new Vector3(1, 1, mainCam.nearClipPlane));
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(p, 0.1F);
    }
}
