using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class CreateRoomRequest :BaseRequest
{
    private RoomPanel roomPanel;
    public override void Awake()
    {
        requestCode = RequestCode.Room;
        actionCode = ActionCode.CreateRoom;
        base.Awake();
    }
    public override void SendRequest()
    {
        base.SendRequest("r");
    }
    public void SetPanel(BasePanel panel)
    {
        roomPanel = panel as RoomPanel;
    }
    public override void OnRespon(string data)
    {
        string[] str = data.Split('-');
        ReturnUserCode usercode = (ReturnUserCode)int.Parse(str[0]);
        RoleType roleType = (RoleType)int.Parse(str[1]);
        gameface.SetCurrentRole(roleType);
        if (usercode == ReturnUserCode.Success)
        {
            if (roomPanel == null)
            {
                roomPanel = GetComponent<RoomPanel>();
            }
            roomPanel.SetLocalPlayerResSync();
        }
    }
}
