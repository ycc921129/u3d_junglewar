using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class UpdateResultRequest : BaseRequest
{
    private int resultCount;
    private int resultWinCount;
    private bool isUpdateResult = false;
    private RoomListPanel roomListPanel;

    public override void Awake()
    {
        actionCode = ActionCode.UpdateResult;
        base.Awake();
        roomListPanel = GetComponent<RoomListPanel>();
    }
    private void Update()
    {
        if (isUpdateResult)
        {
            roomListPanel.UpdateResult(resultCount, resultWinCount);
            isUpdateResult = false;
        }
    }
    public override void OnRespon(string data)
    {
        string[] strs = data.Split('_');
        resultCount = int.Parse(strs[0]);
        resultWinCount = int.Parse(strs[1]);
        isUpdateResult = true;
    }
}
