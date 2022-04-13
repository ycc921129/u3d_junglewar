using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraManager : BaseManager
{
    private GameObject cameraGo;
    private Animator cameraAnim;
    private CameraForllow followTarget;
    private Vector3 originalPosition;
    private Vector3 originalRotation;

    public CameraManager(GameFace _gameface) : base(_gameface)
    {
    }
    public override void OnInit()
    {
        cameraGo = Camera.main.gameObject;
        cameraAnim = cameraGo.GetComponent<Animator>();
        followTarget = cameraGo.GetComponent<CameraForllow>();
    }
    public void FollowRole()
    {
        followTarget.mTarget = gameface.GetCurrentRole().transform;
        originalPosition = cameraGo.transform.position;
        originalRotation = cameraGo.transform.eulerAngles;
        cameraAnim.enabled = false; 
        Quaternion targetQuaternion = Quaternion.LookRotation(followTarget.mTarget.position - cameraGo.transform.position);
        cameraGo.transform.DORotateQuaternion(targetQuaternion, 1.0f).OnComplete(delegate ()
        {
            followTarget.enabled = true;
        });

    }
    public void WalkthroughScene()
    {
        followTarget.enabled = false;
        cameraGo.transform.DOMove(originalPosition, 1f);
        cameraGo.transform.DORotate(originalRotation, 1f).OnComplete(delegate ()
        {
            cameraAnim.enabled = true;
        });
    }
}
