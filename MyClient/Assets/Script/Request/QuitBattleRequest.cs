using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class QuitBattleRequest : BaseRequest
{
    private GamePanel gamePanel;
    private bool isQuitBattle = false;
    public override void Awake()
    {
        requestCode = RequestCode.Game;
        actionCode = ActionCode.QuitBattle;
        base.Awake();
        gamePanel = GetComponent<GamePanel>();
    }
    public override void SendRequest()
    {
        base.SendRequest("r");
    }
    private void Update()
    {
        if (isQuitBattle)
        {
            gamePanel.QuitBattle();
            isQuitBattle = false;
        }
    }
    public override void OnRespon(string data)
    {
        isQuitBattle = true;
    }
}
