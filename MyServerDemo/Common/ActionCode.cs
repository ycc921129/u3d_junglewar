using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public enum ActionCode
    {
        None,
        Login,
        Register,
        RoomList,
        ListRoom,
        CreateRoom,
        JoinRoom,
        UpdateRoom,
        QuitRoom,
        StartGame,
        ShowTimer,
        StartPlay,
        Move,
        Shoot,
        Attack,
        GameOver,
        UpdateResult,
        QuitBattle
    }
}
