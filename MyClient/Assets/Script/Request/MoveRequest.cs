using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class MoveRequest : BaseRequest
{
    private Transform localTransform;
    private PlayerMove playerMove;
    private float syncData = 30.0f;

    private Transform otherTransform;
    private Animator otherAnimator;

    private bool isSyncRemotePlayer = false;
    private Vector3 pos;
    private Vector3 rot;
    private float forword;

    public override void Awake()
    {
        requestCode = RequestCode.Game;
        actionCode = ActionCode.Move;
        base.Awake();
    }
    private void Start()
    {
        InvokeRepeating("SendRequestSync", 0, 1 / syncData);
    }
    private void FixedUpdate()
    {
        if (isSyncRemotePlayer)
        {
            SyncRemotePlayer();
            isSyncRemotePlayer = false;
        }
    }
    public MoveRequest SetTransformAndPlayerMove(Transform _localTransform,PlayerMove _playerMove)
    {
        localTransform = _localTransform;
        playerMove = _playerMove;
        return this;
    }
    public MoveRequest SetOtherTransformAndPlayerMove(Transform _otherTransform)
    {
        otherTransform = _otherTransform;
        otherAnimator = otherTransform.GetComponent<Animator>();
        return this;
    }
    public void SendRequestSync()
    {
        SendRequest(localTransform.position.x, localTransform.position.y, localTransform.position.z,
            localTransform.eulerAngles.x, localTransform.eulerAngles.y, localTransform.eulerAngles.z, playerMove.forword);
    }
    private void SendRequest(float posX,float posY,float posZ,float rotX,float rotY,float rotZ,float forword)
    {
        string data = string.Format("{0}_{1}_{2},{3}_{4}_{5},{6}", posX, posY, posZ, rotX, rotY, rotZ, forword);
        base.SendRequest(data);
    }
    public override void OnRespon(string data)
    {
        string[] strs = data.Split(',');
        pos = Pause(strs[0]);
        rot = Pause(strs[1]);
        forword = float.Parse(strs[2]);
        isSyncRemotePlayer = true;
    }
    private Vector3 Pause(string str)
    {
        string[] strs = str.Split('_');
        return new Vector3(float.Parse(strs[0]), float.Parse(strs[1]), float.Parse(strs[2]));
    }
    private void SyncRemotePlayer()
    {
        otherTransform.position = pos;
        otherTransform.eulerAngles = rot;
        otherAnimator.SetFloat("Forward", forword);
    }
}
