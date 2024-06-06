using KohonenMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7
{
    public class KohonenNet
    {
        List<Neuron> neuron = new List<Neuron>();
        double[] ps;

        public List<Neuron> Neurons { get { return neuron; } }

        public KohonenNet(int input, int output) 
        { 
            for (int i = 0; i<output; i++)
            {
                Neuron n = new Neuron(input);
                neuron.Add(n);
            }

            ps = new double[output];    
        }

        public bool IsCompetitive(Neuron n) 
        {
            return !(ps[neuron.IndexOf(n)] < Pmin());
        }

        public Neuron Winner(double[] xs, bool isP)
        {
            double min = double.MaxValue;
            int minI = (int) (neuron.Count * MyRandom.Rand());

            for (int i = 0; i < this.neuron.Count; i++)
            {
                if (isP && !IsCompetitive(this.neuron[i])) continue;

                double res = neuron[i].D(xs);

                if (min > res)
                {
                    minI = i;
                    min = res;
                }
            }

            return neuron[minI];
        }

        public List<Neuron> AllChangers(Neuron winner, double t, double T) 
        {
            List<Neuron> changers = new List<Neuron>();

            for (int i = 0; i < this.neuron.Count; i++)
            {
                double d = this.neuron[i].D(winner.Ws);

                if (d < SigmaFunction(t,T))
                {
                    changers.Add(this.neuron[i]);
                }
            }

            return changers;
        }

        public double E(List<double[]> dataSet)
        {
            double sum = 0;
            int q = dataSet.Count;

            for (int i = 0; i < q; i++)
            {
                Neuron winner = Winner(dataSet[i], false);
                double normaSq = 0;

                for (int j = 0; j < winner.Ws.Count(); j++)
                {
                    normaSq += (winner.Ws[j] - dataSet[i][j]) * (winner.Ws[j] - dataSet[i][j]);
                }
                sum += normaSq;
            }

            return sum / q;
        }

        public void Recount(Neuron winner, List<Neuron> seconders, double[] xs, double t, double T)
        {
            for(int i = 0; i < seconders.Count;i++)
            {
                double n = N(seconders[i].D(winner.Ws), t, T);

                for (int j = 0; j < seconders[i].Ws.Length; j++) 
                {
                    double w = seconders[i].Ws[j];
                    double x = xs[j];

                    seconders[i].Ws[j] = w + (x - w) * n;
                }
            }
        }

        public double Pmin()
        {
            return 0.74;
        }

        public void P(Neuron winner, int t)
        {
            int l = this.ps.Length;

            if (t == 0)
            {
                for (int i = 0; i < l; i++)
                {
                    this.ps[i] = 1.0 / l;
                }
            }
            else
            {
                double min = Pmin();

                for (int i = 0; i < l; i++)
                {
                    if (this.neuron[i] == winner)
                    {
                        if (this.ps[i] > min) this.ps[i] = this.ps[i] - min;
                        else this.ps[i] = 0;
                    }
                    else
                    {
                        if (this.ps[i] < 1.0 - 1.0 / l) this.ps[i] = this.ps[i] + 1.0 / l;
                        else this.ps[i] = 1;
                    }
                }
            }
        }

        public double N(double d, double t, double T)
        {
            return GaussFunction(d, t, T) * A(t);
        }

        public double A(double t)
        {
            double a = 1;
            double b = 3;

            return a / (t + b);
        }
        public double GaussFunction(double d, double t, double T)
        {
            return Math.Pow(Math.E, -(d / (2 * SigmaFunction(t, T))));
        }

        public double SigmaFunction(double t, double T)
        {
            return Math.Sqrt(MainWindow.Input) * (1 - t / T);
        }
    }
}
