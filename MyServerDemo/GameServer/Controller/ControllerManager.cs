using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.Reflection;
using GameServer.Servers;

/// <summary>
/// Controller管理类 一个功能对应一个Controller
/// </summary>
namespace GameServer.Controller
{
    class ControllerManager
    {
        private Dictionary<RequestCode, BaseContoller> controllerDic = new Dictionary<RequestCode, BaseContoller>();
        private Server server;

        public ControllerManager(Server _server)
        {
            this.server = _server;
            InitController();
        }
        public void InitController()
        {
            DefultController defultController = new DefultController();
            controllerDic.Add(defultController.GetRequestCode, defultController);
            controllerDic.Add(RequestCode.User, new UserController());
            controllerDic.Add(RequestCode.Room, new RoomController());
            controllerDic.Add(RequestCode.Game, new GameController());
        }
        public void HandleRequest(RequestCode requestCode,ActionCode actionCode,string data,Client client)
        {
            BaseContoller controller;
            if (controllerDic.TryGetValue(requestCode,out controller))
            {
                string methodName = Enum.GetName(typeof(ActionCode), actionCode);
                MethodInfo methodInfo = controller.GetType().GetMethod(methodName);
                if (methodInfo == null)
                {
                    Console.WriteLine("在[" + controller + "]中没有对应的方法[" + actionCode + "]");
                }
                object[] parameters = new object[] { data, client, server };
                object ob = methodInfo.Invoke(controller, parameters);
                if (ob == null || string.IsNullOrEmpty(ob as string))
                {
                    return;
                }
                server.SendRequest(client, actionCode, ob as string);
            }
            else
            {
                Console.WriteLine("没有找到[" + requestCode + "]对应的controller");
                return;
            }            
        }
    }
}
