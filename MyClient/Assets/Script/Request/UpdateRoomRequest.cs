using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class UpdateRoomRequest : BaseRequest
{
    private RoomPanel roomPanel;
    public override void Awake()
    {
        actionCode = ActionCode.UpdateRoom;
        requestCode = RequestCode.Room;
        roomPanel = GetComponent<RoomPanel>();
        base.Awake();
    }
    public override void OnRespon(string data)
    {
        UserData ud1 = null;
        UserData ud2 = null;
        string[] strs = data.Split('|');
        ud1 = new UserData(strs[0]);
        if (strs.Length > 1)
        {
            ud2 = new UserData(strs[1]);
        }
        roomPanel.SetAllPlayerResSync(ud1, ud2);
    }
}
