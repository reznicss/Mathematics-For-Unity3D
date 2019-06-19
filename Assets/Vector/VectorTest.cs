
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorTest : MonoBehaviour {

	void Start () {
        var v1 = new Vector3(1, 1, 0);
        var v2 = new Vector3(1, 0, 0);

        print("*******向量长度*******");
        print(v1.magnitude + "_" + Math3D.V3Magnitude(v1));

        print("*******向量长度平方*******");
        print(v1.sqrMagnitude + "_" + Math3D.V3SqrMagnitude(v1));

        print("*******向量点乘(内积)*******");
        print(Vector3.Dot(v1,v2) + "_" + Math3D.V3Dot(v1,v2));

        print("*******向量夹角()*******");
        print(Vector3.Angle(v1, v2) + "_" + Math3D.V3Angle(v1, v2));

    }

    // Update is called once per frame
    void Update () {
		
	}
}
