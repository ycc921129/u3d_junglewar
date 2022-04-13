using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using GameServer.Servers;
using GameServer.DAO;
using GameServer.Model;

namespace GameServer.Controller
{
    class UserController:BaseContoller
    {
        private UserDAO usedao = new UserDAO();
        private ResultDAO resultdao = new ResultDAO();
        public UserController()
        {
            requestCode = RequestCode.User;
        }
        public string Login(string data,Client client,Server server)
        {
            string[] userandpwd = data.Split('-');
            User us = usedao.VerifyUser(client.GetConnection, userandpwd[0], userandpwd[1]);
            if (us == null)
            {
                //Enum.GetName(typeof(ReturnUserCode), ReturnUserCode.Fail);
                return ((int)ReturnUserCode.Fail).ToString();
            }
            else
            {
                Result reslut = resultdao.GetResultValue(client.GetConnection, us.id);
                client.Setuserdata(us, reslut);
                Console.WriteLine("Reslut = " + ((int)ReturnUserCode.Success).ToString() + ","+ us.username+","+ reslut.resultCount+","+reslut.resultWinCount);
                return string.Format("{0}-{1}-{2}-{3}", ((int)ReturnUserCode.Success).ToString(),us.username, reslut.resultCount, reslut.resultWinCount);
            }
        }
        public string Register(string data,Client client,Server server)
        {
            string[] userandpwd = data.Split('-');
            string username = userandpwd[0];
            string password = userandpwd[1];
            bool isHavausername = usedao.GetDataUserName(client.GetConnection, username);
            if (isHavausername)
            {
                return ((int)ReturnUserCode.Fail).ToString();
            }

            usedao.AddDatasInDAO(client.GetConnection, username, password);
            return ((int)ReturnUserCode.Success).ToString();
        }
    }
}
