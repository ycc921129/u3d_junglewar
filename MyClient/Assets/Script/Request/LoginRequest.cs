using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class LoginRequest : BaseRequest
{
    private LoginPanel loginPanel;
    public override void Awake()
    {
        requestCode = RequestCode.User;
        actionCode = ActionCode.Login;
        loginPanel = GetComponent<LoginPanel>();
        base.Awake();
    }
    public void SendRequest(string username, string password)
    {
        string data = username + "-" + password;
        base.SendRequest(data);
    }
    public override void OnRespon(string data)
    {
        string[] strs = data.Split('-');
        Debug.Log("strs[0] = " + strs[0]);
        ReturnUserCode returnCode = (ReturnUserCode)int.Parse(strs[0]);
        loginPanel.LoginRequest(returnCode);
        if (returnCode == ReturnUserCode.Success)
        {
            int id = int.Parse(strs[0]);
            string username = strs[2];
            int resultCount = int.Parse(strs[2]);
            int resultWinCount = int.Parse(strs[3]);
            UserData userdata = new UserData(id, username, resultCount, resultWinCount);
            gameface.SetUserdata(userdata);
        }
    }
}
