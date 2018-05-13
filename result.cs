using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace game_15
{
    [Serializable]
    public class result
    {
        public string Name
        { get; set; }

        public DateTime StartTime
        { get; set; }

        public TimeSpan GameTime
        { get; set; }

        public int Shifts
        { get; set; }
    }
}
