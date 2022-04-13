using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;

public class GameOverRequest : BaseRequest
{
    private GamePanel gamePanel;
    private bool isGameover = false;
    private ReturnUserCode returnUserCode;
    public override void Awake()
    {
        requestCode = RequestCode.Game;
        actionCode = ActionCode.GameOver;
        gamePanel = GetComponent<GamePanel>();
        base.Awake();
    }
    private void Update()
    {
        if (isGameover)
        {
            gamePanel.SetResult(returnUserCode);
            isGameover = false;
        }
    }
    public override void OnRespon(string data)
    {
        returnUserCode = (ReturnUserCode)int.Parse(data);
        isGameover = true;
    }
}
