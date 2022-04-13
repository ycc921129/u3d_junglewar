using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour
{
    public Text username;
    public Text TotalCount;
    public Text WinCount;
    public Button JointBt;

    private RoomListPanel roomListPanel;
    private int id;
	private void Start()
    {
        if (JointBt != null)
        {
            JointBt.onClick.AddListener(JointRoom);
        }
    }
    public void SetRoomInfo(int id,string username, int totalCount, int winCount, RoomListPanel roomListPanel)
    {
        SetRoomInfo(id, username, totalCount.ToString(), winCount.ToString(), roomListPanel);
    }
    public void SetRoomInfo(int id,string username, string totalCount, string winCount,RoomListPanel roomListPanel)
    {
        this.id = id;
        this.username.text = username;
        this.TotalCount.text = "总场数\n" + totalCount;
        this.WinCount.text = "胜利\n" + winCount;
        this.roomListPanel = roomListPanel;
    }
    private void JointRoom()
    {
        roomListPanel.OnclickJoinRoom(id);
    }

    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
