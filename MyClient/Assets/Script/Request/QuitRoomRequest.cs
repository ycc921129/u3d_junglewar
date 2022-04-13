using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class QuitRoomRequest : BaseRequest
{
    private RoomPanel roomPanel;
    public override void Awake()
    {
        requestCode = RequestCode.Room;
        actionCode = ActionCode.QuitRoom;
        roomPanel = GetComponent<RoomPanel>();
        base.Awake();
    }
    public override void SendRequest()
    {
        base.SendRequest("r");
    }
    public override void OnRespon(string data)
    {
        ReturnUserCode returnUsercode = (ReturnUserCode)int.Parse(data);
        if (returnUsercode == ReturnUserCode.Success)
        {
            roomPanel.OnExitResponse();
        }
    }
}
