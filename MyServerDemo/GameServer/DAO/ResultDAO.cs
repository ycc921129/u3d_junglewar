using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using GameServer.Model;

namespace GameServer.DAO
{
    class ResultDAO
    {
        public Result GetResultValue(MySqlConnection conn, int userid)
        {
            MySqlDataReader reader = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from resultlist where userid=@us", conn);
                cmd.Parameters.AddWithValue("us", userid);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int id = reader.GetInt32("id");
                    int resultWindCount = reader.GetInt32("resultWindCount");
                    int resultCount = reader.GetInt32("resultCount");
                    Result result = new Result(id, userid, resultCount, resultWindCount);
                    return result;
                }
                else
                {
                    Result result = new Result(-1, userid, 0, 0);
                    return result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("查询战绩数据错误");
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return null;           
        }
        public void UpdateOrAddResultValue(MySqlConnection conn, Result res)
        {
            MySqlCommand cmd = null;
            try
            {
                if (res.id <= -1)
                {
                    cmd = new MySqlCommand("insert into resultlist set resultCount=@resultCount,resultWindCount=@resultWindCount,userid=@userid", conn);
                }
                else
                {
                    cmd = new MySqlCommand("update resultlist set resultCount=@resultCount,resultWindCount=@resultWindCount where userid=@userid", conn);
                }
                cmd.Parameters.AddWithValue("resultCount", res.resultCount);
                cmd.Parameters.AddWithValue("resultWindCount", res.resultWinCount);
                cmd.Parameters.AddWithValue("userid", res.userid);
                cmd.ExecuteNonQuery();
                if (res.id <= -1)
                {
                    Result tmpRes = GetResultValue(conn, res.userid);
                    res.id = tmpRes.id;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("更新战绩失败" + e);
            }

        }
    }
}
