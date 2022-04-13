using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace MySql数据库
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = "database=test01;datasource=127.0.0.1;port=3306;user=root;password=1234";
            MySqlConnection connection = new MySqlConnection(str);
            connection.Open();
            #region 查询
            //MySqlCommand cmd = new MySqlCommand("select * from users", connection);
            //MySqlDataReader datareader = cmd.ExecuteReader();
            //while (datareader.Read())
            //{
            //    string username = datareader.GetString("username");
            //    string password = datareader.GetString("password");
            //    Console.WriteLine("username=" + username + ",password=" + password);
            //}
            //datareader.Close();
            #endregion
            #region 插入
            //string username = "adf";
            //string password = "asdfasdf';delete from users;";
            ////MySqlCommand cmd = new MySqlCommand("insert into users set username = " + "'" + username + "'" + ",password = " + "'" + password + "'", connection);
            //MySqlCommand cmd = new MySqlCommand("insert into users set username=@user,password=@pwd", connection);
            //cmd.Parameters.AddWithValue("user", username);
            //cmd.Parameters.AddWithValue("pwd", password);
            //cmd.ExecuteNonQuery();            
            #endregion
            #region 删除
            //MySqlCommand cmd = new MySqlCommand("delete from users where id = 11", connection);
            //cmd.ExecuteNonQuery();
            #endregion 
            #region 更新
            //MySqlCommand cmd = new MySqlCommand("update users set password = @pd where id=15", connection);
            //cmd.Parameters.AddWithValue("pd", "yangchenchao");
            //cmd.ExecuteNonQuery();
            #endregion 
            connection.Close();
        }
    }
}
