using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraForllow : MonoBehaviour
{
    public Transform mTarget;
    public Vector3 mOffset = new Vector3(0, 9.699244f, -14.30557f);
		
	void Update()  
    {
        transform.position = mTarget.position + mOffset;
        transform.LookAt(mTarget);  
	}
}
