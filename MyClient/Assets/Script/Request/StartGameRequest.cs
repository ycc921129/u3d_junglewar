using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class StartGameRequest : BaseRequest
{
    private RoomPanel roomPanel;
    public override void Awake()
    {
        requestCode = RequestCode.Game;
        actionCode = ActionCode.StartGame;
        base.Awake();
        roomPanel = GetComponent<RoomPanel>();
    }
    public override void SendRequest()
    {
        base.SendRequest("r");
    }
    public override void OnRespon(string data)
    {
        ReturnUserCode userCode = (ReturnUserCode)int.Parse(data);
        roomPanel.OnResponStartGameRequest(userCode);
    }
}
