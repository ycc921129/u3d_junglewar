using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomPanel : BasePanel
{
    private Text BUsername;
    private Text BTotalCount;
    private Text BWinCount;
    private Text RUsername;
    private Text RTotalCount;
    private Text RWinCount;
    private UserData ud = null;
    private UserData ud1 = null;
    private UserData ud2 = null;
    private QuitRoomRequest quitRoomRequest;
    private StartGameRequest startGameRequest;
    private StartPlayRequest startPlayRequest;
    private bool isQuitroom = false;
    private void Start()
    {
        BUsername = transform.Find("BluePanel/Username").GetComponent<Text>();
        BTotalCount = transform.Find("BluePanel/TotalCount").GetComponent<Text>();
        BWinCount = transform.Find("BluePanel/WinCount").GetComponent<Text>();
        RUsername = transform.Find("RedPanel/Username").GetComponent<Text>();
        RTotalCount = transform.Find("RedPanel/TotalCount").GetComponent<Text>();
        RWinCount = transform.Find("RedPanel/WinCount").GetComponent<Text>();
        transform.Find("StartButton").GetComponent<Button>().onClick.AddListener(StartGame);
        transform.Find("ExitButton").GetComponent<Button>().onClick.AddListener(EndRoom);
        quitRoomRequest = GetComponent<QuitRoomRequest>();
        startGameRequest = GetComponent<StartGameRequest>();
        startPlayRequest = GetComponent<StartPlayRequest>();
    }
    private void Update()
    {
        if (ud != null)
        {
            SetLocalPlayerRes(ud.username, ud.resultCount.ToString(), ud.resultWinCount.ToString());
            ClearEnemyPlayerRes();
            ud = null;
        }
        if (ud1 != null)
        {
            Debug.Log("ud1 = " + ud1);
            SetLocalPlayerRes(ud1.username, ud1.resultCount.ToString(), ud1.resultWinCount.ToString());
            if (ud2 != null)
            {
                Debug.Log("ud2 = " + ud2);
                SetEnemyPlayerRes(ud2.username, ud2.resultCount.ToString(), ud2.resultWinCount.ToString());
            }
            else
            {
                Debug.Log("ud2 = " + ud2);
                ClearEnemyPlayerRes();
            }
            ud1 = null;
            ud2 = null;
        }
        if (isQuitroom)
        {
            isQuitroom = false;
            mUIManager.PushPanel(UIPanelType.RoomListPanel);
        }
    }
    public void SetLocalPlayerResSync()
    {
        ud = facade.GetUserData();
    }
    public void SetAllPlayerResSync(UserData ud1, UserData ud2)
    {
        this.ud1 = ud1;
        this.ud2 = ud2;
    }
    public void SetLocalPlayerRes(string username, string totalCount, string winCount)
    {
        BUsername.text = username;
        BTotalCount.text = "总场数：" + totalCount;
        BWinCount.text = "胜利：" + winCount;
    }
    private void SetEnemyPlayerRes(string username, string totalCount, string winCount)
    {
        RUsername.text = username;
        RTotalCount.text = "总场数：" + totalCount;
        RWinCount.text = "胜利：" + winCount;
    }
    public void ClearEnemyPlayerRes()
    {
        Debug.Log("刷新房间");
        RUsername.text = "";
        RTotalCount.text = "等待玩家加入....";
        RWinCount.text = "";
    }
    public override void OnEnter()
    {
        base.OnEnter();
    }
    public override void OnPause()
    {
        base.OnPause();
    }
    public override void OnResume()
    {
        base.OnResume();
    }
    public override void OnExit()
    {
        base.OnExit();
    }
    public void OnExitResponse()
    {
        isQuitroom = true;
    }
    private void StartGame()
    {
        startGameRequest.SendRequest();
    }
    public void OnResponStartGameRequest(ReturnUserCode code)
    {
        if (code == ReturnUserCode.Fail)
        {
            mUIManager.ShowMessageSync("你不是房主，无法开启游戏");
        }
        else
        {
            mUIManager.ShowMessageSync("开始游戏倒计时");
            mUIManager.PushPanelSync(UIPanelType.GamePanel);
            facade.EntingPlaySync();
        }
    }
    private void EndRoom()
    {
        quitRoomRequest.SendRequest();
    }
}
