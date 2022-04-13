using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using GameServer.Servers;

namespace GameServer.Controller
{
    class RoomController : BaseContoller
    {
        public RoomController()
        {
            requestCode = RequestCode.Room;
        }
        public string CreateRoom(string data, Client client, Server server)
        {
            server.CreateRoom(client);
            return ((int)ReturnUserCode.Success).ToString() + "-" +((int)RoleType.Blue);
        }
        public string ListRoom(string data,Client cilent,Server server)
        {
            StringBuilder str = new StringBuilder();
            foreach (Room room in server.GetRoomList())
            {
                if (room.IsWaitingJoin())
                {
                    str.Append(room.GetHouseOwnerData() + "|");
                }
            }
            if (str.Length == 0)
            {
                return "0";
            }
            if (str.Length > 0)
            {
                str.Remove(str.Length - 1, 1);
            }
            return str.ToString();
        }
        public string JoinRoom(string data,Client client,Server server)
        {
            int id = int.Parse(data);
            Room room = server.GetRoomById(id);
            if (room == null)
            {
                return ((int)ReturnUserCode.NotFound).ToString();
            }
            else if (room.IsWaitingJoin() == false)
            {
                return ((int)ReturnUserCode.Fail).ToString();
            }
            else
            {
                room.AddClient(client);
                string roomData = room.GetRoomData();
                room.BroadCasetMsg(client, ActionCode.UpdateRoom, roomData);
                return ((int)ReturnUserCode.Success).ToString() + "-" + ((int)RoleType.Red) + "," + roomData;
            }
        }
        public string QuitRoom(string data,Client client,Server server)
        {
            bool isHouseOwner = client.IsHouseOwner();
            Room room = client.Room;
            if (isHouseOwner)
            {
                room.BroadCasetMsg(client, ActionCode.QuitRoom, ((int)ReturnUserCode.Success).ToString());
                room.Close();
                return ((int)ReturnUserCode.Success).ToString();
            }
            else
            {
                client.Room.RemoveClient(client);
                room.BroadCasetMsg(client, ActionCode.UpdateRoom, room.GetRoomData());
                return ((int)ReturnUserCode.Success).ToString();
            }
        }
    }
}
