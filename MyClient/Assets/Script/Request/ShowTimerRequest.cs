using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class ShowTimerRequest : BaseRequest
{
    private GamePanel gamePanel;
    public override void Awake()
    {
        actionCode = ActionCode.ShowTimer; 
        base.Awake();
        gamePanel = GetComponent<GamePanel>();
    }
    public override void OnRespon(string data)
    {
        gamePanel.ShowTimeAsync(int.Parse(data));
    }
}
