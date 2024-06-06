using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace KohonenMap
{
    public class Cell
    {
        public readonly static double Size = 20;

        Neuron neuron;
        Polygon polygon;
        
        double p;

        public Cell(Neuron neuron, Polygon polygon)
        {
            this.polygon = polygon;
            this.neuron = neuron;
            this.p = 0;
        }

        public double P { get { return p; } set { p = value; } }
        public Neuron Neuron { get { return neuron; } }
        public Polygon Polygon { get { return polygon; } }

    }
}
