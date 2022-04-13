using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class ListRoomRequest : BaseRequest
{
    private RoomListPanel roomListPanel;
    public override void Awake()
    {
        requestCode = RequestCode.Room;
        actionCode = ActionCode.ListRoom;
        base.Awake();
        roomListPanel = GetComponent<RoomListPanel>();
    }

    public override void SendRequest()
    {
        base.SendRequest("r");
    }
    public override void OnRespon(string data)
    {
        Debug.Log("data + " + data);
        List<UserData> udList = new List<UserData>();
        if (data != "0")
        {
            string[] str = data.Split('|');
            foreach (string id in str)
            {
                string[] strs = id.Split('-');
                udList.Add(new UserData(int.Parse(strs[0]),strs[1], int.Parse(strs[2]), int.Parse(strs[3])));
            }
        }
        roomListPanel.LoadRoomItemSync(udList);
        Debug.Log("_udList.Count + " + udList.Count);
    }
}
