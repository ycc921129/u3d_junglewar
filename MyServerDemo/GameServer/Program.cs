﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Servers;

namespace GameServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server("192.168.0.101", 5000);
            server.Start();

            Console.ReadKey();
        }
    }
}
