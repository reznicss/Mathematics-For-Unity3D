
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorTest : MonoBehaviour
{
    public Transform t1;
    public Transform t2;
    public Vector3 v1;
    public Vector3 v2;


    private void Start()
    {
        print("向量长度:" + v1.magnitude + "_" + Math3D.V3Magnitude(v1));
        print("向量长度平方:" + v1.sqrMagnitude + "_" + Math3D.V3SqrMagnitude(v1));

        print("向量点乘(内积):" + Vector3.Dot(v1, v2) + "_" + Math3D.V3Dot(v1, v2));
        print("向量夹角(0到180):" + Vector3.Angle(v1, v2) + "_" + Math3D.V3Angle(v1, v2));
        print("向量夹角(0到180 带正负):" + Vector3.SignedAngle(v2, v1, Vector3.up) + "_" + Math3D.V3SignedAngle(v2, v1, Vector3.up));
        print("向量夹角(0到360):" + Math3D.V3SignedAngleBetween(v2, v1, Vector3.up));


    }

    void OnDrawGizmos()
    {
        v1 = t1.position;
        v2 = t2.position;
        Gizmos.DrawSphere(v1, 0.05f);
        Gizmos.DrawSphere(v2, 0.05f);
        Gizmos.DrawLine(Vector3.zero, v1);
        Gizmos.DrawLine(Vector3.zero, v2);
        //Gizmos.DrawLine(v1, v2);
    }
}
