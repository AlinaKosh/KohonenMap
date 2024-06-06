using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KohonenMap
{
    public class MyRandom
    {
        static Random random = new Random();

        public static double Rand()
        {
            return random.NextDouble();
        }
    }
}
