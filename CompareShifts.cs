using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_15
{
    public class CompareShifts : IComparer<result>
    {
        public int Compare(result x, result y)
        {
            if (x.Shifts > y.Shifts) return 1;
            else
                if (x.Shifts < y.Shifts) return -1;
            else return 0;
        }
    }
}
