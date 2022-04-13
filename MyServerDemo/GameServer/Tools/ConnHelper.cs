using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace GameServer.Tools
{
    class ConnHelper
    {
        public const string connectionStr = "database=gamedemo1;datasource=127.0.0.1;port=3306;user=root;pwd=1234";
        public static MySqlConnection ConnectDOA()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(connectionStr);
                connection.Open();
                return connection;
            }
            catch (Exception e)
            {
                Console.WriteLine("数据库连接异常 + " + e);
                return null;
            }
        }
        public static void CloseDOA(MySqlConnection connection)
        {
            if (connection!= null)
            {
                connection.Close();
            }
        }
    }
}
