using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class ShootRequest : BaseRequest
{
    [HideInInspector]public PlayerManger playerManager;
    private bool isShoot = false;
    private RoleType rt;
    private Vector3 pos;
    private Vector3 rotation;
    public override void Awake()
    {
        requestCode = RequestCode.Game;
        actionCode = ActionCode.Shoot;
        base.Awake();
    }
    private void Update()
    {
        if (isShoot)
        {
            playerManager.RemoteShoot(rt, pos, rotation);
            isShoot = false;
        }
    }
    public void SendRequest(RoleType roleType,Vector3 pos,Vector3 rot)
    {
        string data = string.Format("{0},{1}_{2}_{3},{4}_{5}_{6}", (int)roleType, pos.x, pos.y, pos.z, rot.x, rot.y, rot.z);
        base.SendRequest(data);
    }
    public override void OnRespon(string data)
    {
        string[] strs = data.Split(',');
        rt = (RoleType)int.Parse(strs[0]);
        pos = Pause(strs[1]);
        rotation = Pause(strs[2]);
        isShoot = true;
    }
    private Vector3 Pause(string str)
    {
        string[] strs = str.Split('_');
        return new Vector3(float.Parse(strs[0]), float.Parse(strs[1]), float.Parse(strs[2]));
    }
}
