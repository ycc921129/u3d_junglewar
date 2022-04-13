using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class JoinRoomRequest : BaseRequest
{
    private RoomListPanel roomListPanel;
    private UserData d1;
    private UserData d2;

    public override void Awake()
    {
        requestCode = RequestCode.Room;
        actionCode = ActionCode.JoinRoom;
        roomListPanel = GetComponent<RoomListPanel>();
        base.Awake();
    }

    public void SendRequest(int id)
    {
        base.SendRequest(id.ToString());
    }
    public override void OnRespon(string data)
    {
        string[] strs = data.Split(',');
        string[] strs2 = strs[0].Split('-');
        ReturnUserCode returnUsercode = (ReturnUserCode)int.Parse(strs2[0]);
        UserData ud1 = null;
        UserData ud2 = null;
        if (returnUsercode == ReturnUserCode.Success)
        {
            string[] udStrarray = strs[1].Split('|');
            ud1 = new UserData(udStrarray[0]);
            ud2 = new UserData(udStrarray[1]);

            RoleType roleType = (RoleType)int.Parse(strs2[1]);
            gameface.SetCurrentRole(roleType);
        }
        roomListPanel.OnJoinResponse(returnUsercode, ud1, ud2);
    }
}
