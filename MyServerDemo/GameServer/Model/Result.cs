using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Model
{
    class Result
    {
        public int id { get; set; }
        public int userid { get; set; }
        public int resultCount { get; set; }
        public int resultWinCount { get; set; }
        public Result(int _id,int _userid,int _resuoltcount,int _resultWincount)
        {
            this.id = _id;
            this.userid = _userid;
            this.resultCount = _resuoltcount;
            this.resultWinCount = _resultWincount;
        }
    }
}
