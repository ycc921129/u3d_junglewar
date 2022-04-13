using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;

public class RoomListPanel : BasePanel
{
    public VerticalLayoutGroup Layout;
    private CreateRoomRequest createRoomrequest;
    private ListRoomRequest listRoomrequest;
    private JoinRoomRequest joinRoomrequest;
    private List<UserData> udList = null;
    private GameObject prefab = null;
    private UserData ud1 = null;
    private UserData ud2 = null;

    private void Start()
    {
        transform.Find("RoomList/CloseButton").GetComponent<Button>().onClick.AddListener(() => mUIManager.PushPanel(UIPanelType.LoginPanel));
        transform.Find("RoomList/CreateRoomButton").GetComponent<Button>().onClick.AddListener(CreateRoom);
        transform.Find("RoomList/RefreshButton").GetComponent<Button>().onClick.AddListener(RefreshBt);
        prefab = Resources.Load<GameObject>("UI/RoomItem");
        if (Layout == null)
        {
            Layout = GetComponentInChildren<VerticalLayoutGroup>();
        }
        createRoomrequest = GetComponent<CreateRoomRequest>();
        listRoomrequest = GetComponent<ListRoomRequest>();
        joinRoomrequest = GetComponent<JoinRoomRequest>();
    }

    public override void OnEnter()
    {
        base.OnEnter();
        InitInfo();
        if (listRoomrequest == null)
        {
            listRoomrequest = GetComponent<ListRoomRequest>();
        }
        listRoomrequest.SendRequest();
    }
    public override void OnPause()
    {
        base.OnPause();
    }
    public override void OnResume()
    {
        base.OnResume();
        listRoomrequest.SendRequest();
    }
    public override void OnExit()
    {
        base.OnExit();
    }
    private void Update()
    {
        if (udList != null)
        {
            LoadRoomItem(udList);
            udList = null;
        }
        if (ud1 != null && ud2 != null)
        {
            BasePanel panel = mUIManager.PushPanel(UIPanelType.RoomPanel);
            (panel as RoomPanel).SetAllPlayerResSync(ud1, ud2);
            ud1 = null;
            ud2 = null;
        }
    }
    public void UpdateResult(int resultcount,int resultWincount)
    {
        facade.UpdateResult(resultcount, resultWincount);
        InitInfo();
    }
    private void InitInfo()
    {
        UserData userdata = facade.GetUserData();
        transform.Find("BattleRes/Username").GetComponent<Text>().text = userdata.username;
        transform.Find("BattleRes/TotalCount").GetComponent<Text>().text = "战斗场数：" + userdata.resultCount;
        transform.Find("BattleRes/WinCount").GetComponent<Text>().text = "胜利场数：" + userdata.resultWinCount;
    }
    public void LoadRoomItemSync(List<UserData> _udList)
    {
        this.udList = _udList;
    }
    private void LoadRoomItem(List<UserData> _udList)
    {
        RoomItem[] roomItem = Layout.GetComponentsInChildren<RoomItem>();
        for (int i = 0; i < roomItem.Length; i++)
        {
            roomItem[i].DestroySelf();
        }

        for (int i = 0; i < udList.Count; i++)
        {
            GameObject go = Instantiate(prefab);
            go.transform.SetParent(Layout.transform);
            UserData ud = _udList[i];
            go.GetComponent<RoomItem>().SetRoomInfo(ud.id, ud.username, ud.resultCount, ud.resultWinCount, this);
        }
        int length = GetComponentsInChildren<RoomItem>().Length;
        Vector2 size = Layout.GetComponent<RectTransform>().sizeDelta;
        Layout.GetComponent<RectTransform>().sizeDelta = new Vector2(size.x,length * ( prefab.GetComponent<RectTransform>().sizeDelta.y + Layout.spacing));
    }
    private void CreateRoom()
    {
        BasePanel panel = mUIManager.PushPanel(UIPanelType.RoomPanel);
        createRoomrequest.SetPanel(panel);
        createRoomrequest.SendRequest();
    }
    private void RefreshBt()
    {
        listRoomrequest.SendRequest();
    }
    public void OnclickJoinRoom(int id)
    {
        joinRoomrequest.SendRequest(id);
    }
    public void OnJoinResponse(ReturnUserCode returncode,UserData data1,UserData data2)
    {
        switch (returncode)
        {
            case ReturnUserCode.NotFound:
                mUIManager.ShowMessageSync("房间销毁无法加入");
                break;
            case ReturnUserCode.Fail:
                mUIManager.ShowMessageSync("房间已满无法加入");
                break;
            case ReturnUserCode.Success:
                this.ud1 = data1;
                this.ud2 = data2;
                break; 
            default:
                break;
        }
    }
}
