using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;


namespace game_15
{
    public class CompareTime : IComparer<result>
    {
        public int Compare(result x, result y) 
        {
            if (x.StartTime < y.StartTime) return 1;
            else
                if (x.StartTime > y.StartTime) return -1;
            else return 0;
        }
    }
}
