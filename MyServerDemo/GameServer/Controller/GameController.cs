using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using GameServer.Servers;

namespace GameServer.Controller
{
    class GameController : BaseContoller
    {
        public GameController()
        {
            requestCode = RequestCode.Game;            
        }

        public string StartGame(string data, Client client, Server server)
        {
            if (client.IsHouseOwner())
            {
                Room room = client.Room;
                room.BroadCasetMsg(client, ActionCode.StartGame, ((int)ReturnUserCode.Success).ToString());
                room.ShowTimer();
                return ((int)ReturnUserCode.Success).ToString();
            }
            else
            {
                return ((int)ReturnUserCode.Fail).ToString();
            }
        }
        public string Move(string data, Client client, Server serv)
        {
            Room room = client.Room;
            if (room != null)
            {
                room.BroadCasetMsg(client, ActionCode.Move, data);
            }
            return null;
        }
        public string Shoot(string data, Client client, Server serv)
        {
            Room room = client.Room;
            if (room != null)
            {
                room.BroadCasetMsg(client, ActionCode.Shoot, data);
            }
            return null;
        }
        public string Attack(string data, Client client, Server serv)
        {
            int damage = int.Parse(data);
            Room room = client.Room;
            if (room == null) return null;
            room.TakeDamage(damage, client);
            return null;
        }
        public string QuitBattle(string data, Client client, Server serv)
        {
            Room room = client.Room;
            if (room != null)
            {
                room.BroadCasetMsg(null, ActionCode.QuitBattle, "r");
                room.Close();
            } 
            return null;
        }
    }
}
