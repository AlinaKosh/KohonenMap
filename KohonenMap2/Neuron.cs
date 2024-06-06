using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KohonenMap
{
    public class Neuron
    {
        double[] ws;

        public Neuron(int count)
        {
            this.ws = new double[count];

            for (int i = 0; i < count; i++)
            {
                this.ws[i] = MyRandom.Rand();
            }
        }

        public double[] Ws
        {
            get { return this.ws; }
            set { this.ws = value; }
        }

        public double D(double[] xs)
        {
            double sum = 0;
            for (int i = 0; i < xs.Length; i++)
            {
                sum += (xs[i] - this.ws[i]) * (xs[i] - this.ws[i]);
            }
            return Math.Sqrt(sum);
        }
    }
}
