using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.Threading;

namespace GameServer.Servers
{
    enum RoomState
    {
        WaitingJoin,
        WaitingBattle,
        Battle,
        End
    }

    class Room
    {
        private List<Client> clientRoom = new List<Client>();
        private RoomState roomstate = RoomState.WaitingJoin;
        private Server server;
        private const int MAX_HP = 200;
        public Room(Server _server)
        {
            this.server = _server;
        }

        public bool IsWaitingJoin()
        {
            return roomstate == RoomState.WaitingJoin;
        }
        public void AddClient(Client client)
        {
            clientRoom.Add(client);
            client.Room = this;
            client.HP = MAX_HP;
            if (clientRoom.Count >= 2)
            {
                roomstate = RoomState.WaitingBattle;
            }
        }
        public void RemoveClient(Client _client)
        {
            _client.Room = null;
            clientRoom.Remove(_client);
            if (clientRoom.Count >= 2)
            {
                roomstate = RoomState.WaitingBattle;
            }
            else
            {
                roomstate = RoomState.WaitingJoin;
            }
        }
        public string GetHouseOwnerData()
        {
            return clientRoom[0].GetUserdata();
        }
        public void Close(Client client)
        {
            if (client == clientRoom[0])
            {
                server.RemoveRoom(this);
            }
            else
            {
                clientRoom.Remove(client);
            }
        }
        public int GetRoomId()
        {
            if (clientRoom.Count > 0)
            {
                return clientRoom[0].GetUserid();
            }
            return -1;
        }
        public string GetRoomData()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Client client in clientRoom)
            {
                sb.Append(client.GetUserdata() + "|");
            }
            if (sb.Length > 0 )
            {
                sb.Remove(sb.Length - 1, 1);
            }
            return sb.ToString();
        }
        public void QuitRoom(Client client)
        {
            if (client == clientRoom[0])
            {
                Close();
            }
            else
                clientRoom.Remove(client);
        }
        public void BroadCasetMsg(Client excludeClient, ActionCode _actioncode,string _msg)
        {
            foreach (Client  client in clientRoom)
            {
                if (client != excludeClient)
                {
                    server.SendRequest(client, _actioncode, _msg);
                }
            }
        }
        public bool IsHouseOwner(Client client)
        {
            return client == clientRoom[0];
        }
        public void Close()
        {
            foreach (Client client in clientRoom)
            {
                client.Room = null;
            }
            server.RemoveRoom(this);
        }
        public void ShowTimer()
        {
            new Thread(RunTime).Start();
        }

        private void RunTime()
        {
            Thread.Sleep(500);
            for (int i = 3; i > 0; i--)
            {
                Thread.Sleep(1000);
                BroadCasetMsg(null, ActionCode.ShowTimer, i.ToString());
            }
            BroadCasetMsg(null, ActionCode.StartPlay, "r");
        }
        public void TakeDamage(int damage,Client excludeClient)
        {
            bool isDie = false;
            foreach (Client client in clientRoom)
            {
                if (client != excludeClient)
                {
                    if (client.TakeDamage(damage))
                    {
                        isDie = true;
                    }
                }
            }

            if (isDie == false) return;
            foreach (Client client in clientRoom)
            {
                if (client.IsDie())
                {
                    client.UpdateResult(false);
                    client.Send(ActionCode.GameOver, ((int)ReturnUserCode.Fail).ToString());
                }
                else
                {
                    client.UpdateResult(true);
                    client.Send(ActionCode.GameOver, ((int)ReturnUserCode.Success).ToString());
                }
            }
            Close();
        }
    }
}
