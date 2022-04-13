using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using GameServer.Model;

namespace GameServer.DAO
{
    class UserDAO
    {
        public User VerifyUser(MySqlConnection conn,string _username,string _password)
        {
            MySqlDataReader datareader = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from users where username=@us and password=@pwd", conn);
                cmd.Parameters.AddWithValue("us", _username);
                cmd.Parameters.AddWithValue("pwd", _password);
                datareader = cmd.ExecuteReader();
                if (datareader.Read())
                {
                    int id = datareader.GetInt32("id");
                    User us = new User(id, _username, _password);
                    return us;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("数据库查询账号密码失败 + " + e);
            }
            finally
            {
                if (datareader != null)
                {
                    datareader.Close();
                }
            }
            return null;
        }

        public bool GetDataUserName(MySqlConnection conn,string _username)
        {
            MySqlDataReader datareader = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from users where username=@us", conn);
                cmd.Parameters.AddWithValue("us", _username);
                datareader = cmd.ExecuteReader();
                if (datareader.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("数据库查询账号失败" + e);
            }
            finally
            {
                if (datareader != null) datareader.Close();
            }
            return false;
        }

        public void AddDatasInDAO(MySqlConnection conn,string _username,string _password)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("insert into users set username=@us,password=@pw", conn);
                cmd.Parameters.AddWithValue("us", _username);
                cmd.Parameters.AddWithValue("pw", _password);                
                cmd.ExecuteNonQuery();               
            }
            catch (Exception e)
            {
                Console.WriteLine("添加数据到数据库失败" + e);
            }
        }
    }
}
