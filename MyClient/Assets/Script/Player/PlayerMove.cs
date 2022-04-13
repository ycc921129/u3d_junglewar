using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Animator mAnimator;
    private float speed = 3.0f;
    public float forword { get; private set; }
	void Start ()
    {
        mAnimator = GetComponent<Animator>();
    }

	void FixedUpdate()
    {
        if (mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Grounded") == false) return;
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        if (Mathf.Abs(x) > 0 || Mathf.Abs(z) > 0)
        {
            transform.Translate(new Vector3(x, 0, z) * speed * Time.deltaTime, Space.World);
            transform.rotation = Quaternion.LookRotation(new Vector3(x, 0, z));
            float res = Mathf.Max(Mathf.Abs(x), Mathf.Abs(z));
            forword = res;
            mAnimator.SetFloat("Forward", res);
        }
        else
        {
            mAnimator.SetFloat("Forward", 0.0f);
        }
    }
}
