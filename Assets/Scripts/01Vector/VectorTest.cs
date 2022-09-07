
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorTest : MonoBehaviour
{
    public Transform t1;
    public Transform t2;

    private void Start()
    {
        print("向量长度:" + t1.position.magnitude + "_" + MathVector.V3Magnitude(t1.position));
        print("向量长度平方:" + t1.position.sqrMagnitude + "_" + MathVector.V3SqrMagnitude(t1.position));
        print("向量距离:" + Vector3.Distance(t1.position,t2.position) + "_" + MathVector.V3Distance(t1.position,t2.position));

        print("向量点乘(内积):" + Vector3.Dot(t1.position, t2.position) + "_" + MathVector.V3Dot(t1.position, t2.position));
        print("向量夹角(0到180):" + Vector3.Angle(t1.position, t2.position) + "_" + MathVector.V3Angle(t1.position, t2.position));
        print("向量夹角(0到180 带正负):" + Vector3.SignedAngle(t2.position, t1.position, Vector3.up) + "_" + MathVector.V3SignedAngle(t2.position, t1.position, Vector3.up));
        print("向量夹角(0到360):" + MathVector.V3SignedAngleBetween(t2.position, t1.position, Vector3.up));

        print("向量投影(a在b上投影):" + Vector3.Project(t1.position, t2.position) + "_" + MathVector.V3Project(t1.position, t2.position));
        print("向量投影(a在法线为n的平面上的投影向量):" + Vector3.ProjectOnPlane(t1.position,Vector3.forward) + "_" + MathVector.V3ProjectOnPlane(t1.position,Vector3.forward));

        print("向量叉乘(外积):" + Vector3.Cross(t1.position,t2.position) + "_" + MathVector.V3Cross(t1.position, t2.position));
        Debug.DrawLine(Vector3.zero, MathVector.V3Cross(t1.position, t2.position), Color.black, 1000f);
        Vector3 v1 = new Vector3(1,2,3);
        v1.Add(new Vector3(1,2,3));
        Debug.Log(v1);

        v1.Add(new Vector3(0, 0, 0),new Vector3(1,1,1));
        Debug.Log(v1);
        
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(t1.position, 0.05f);
        Gizmos.DrawSphere(t2.position, 0.05f);
        Gizmos.DrawLine(Vector3.zero, t1.position);
        Gizmos.DrawLine(Vector3.zero, t2.position);
        //Gizmos.DrawLine(t1.position, t2.position);
    }

    public Vector3 centerPt;
    public float radius;
    void Update()
    {
        // Get the new position for the object.
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 newPos = transform.position + movement;

        // Calculate the distance of the new position from the center point. Keep the direction
        // the same but clamp the length to the specified radius.
        Vector3 offset = newPos - centerPt;
        transform.position = centerPt + Vector3.ClampMagnitude(offset, radius);
    }
    
    
    
}
