using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace KohonenMap
{
    public class ColorService
    {
        public ColorService() { }

        public Color GetColor(double from0to1)
        {
            int curr = (int)Math.Truncate(from0to1 * 1024);

            if (curr < 256)
            {
                return Color.FromRgb(0, (byte) curr, 255);
            }
            else if (curr >= 256 && curr < 512)
            {
                return Color.FromRgb(0, 255, (byte)(511 - curr));
            }
            else if (curr >= 512 && curr < 768) 
            {
                return Color.FromRgb((byte)(curr - 512), 255, 0);
            }
            else
            {
                return Color.FromRgb(255, (byte) (1024 - curr), 0);
            }
        }
    }
}
