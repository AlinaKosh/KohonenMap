using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;

namespace KohonenMap
{
    public class KohonenMap
    {
        Cell[,] map;

        public Cell[,] Map
        {
            get { return map; }
        }

        public KohonenMap(int input, int n, int m)
        {
            this.map = new Cell[n, m];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    map[i, j] = new Cell(new Neuron(input), new Polygon());
                }
            }
        }

        public void InitMap(IAddChild parent)
        {
            double r = Cell.Size;
            double r2 = r / 2;
            double r3 = r * Math.Sqrt(3) / 2;

            int n = map.GetLength(0);
            int m = map.GetLength(1);

            for (int i = 0; i < (n + 1) / 2; i++)
            {
                double yc = r * (3 * i + 1);

                for (int j = 0; j < m; j++)
                {
                    double xc = (2 * j + 1) * r3;

                    Polygon polygon = this.map[2 * i,j].Polygon;
                    polygon.Points.Add(new Point(xc, yc - r));
                    polygon.Points.Add(new Point(xc + r3, yc - r2));
                    polygon.Points.Add(new Point(xc + r3, yc + r2));
                    polygon.Points.Add(new Point(xc, yc + r));
                    polygon.Points.Add(new Point(xc - r3, yc + r2));
                    polygon.Points.Add(new Point(xc - r3, yc - r2));
                    polygon.Stroke = Brushes.Black;
                    polygon.StrokeThickness = 1;

                    parent.AddChild(polygon);
                }
            }

            for (int i = 0; i < n / 2; i++)
            {
                double yc = r * (3 * i + 2.5);

                for (int j = 0; j < m; j++)
                {
                    double xc = r3 * (2 * j + 2);

                    Polygon polygon = this.map[2 * i + 1, j].Polygon;
                    polygon.Points.Add(new Point(xc, yc - r));
                    polygon.Points.Add(new Point(xc + r3, yc - r2));
                    polygon.Points.Add(new Point(xc + r3, yc + r2));
                    polygon.Points.Add(new Point(xc, yc + r));
                    polygon.Points.Add(new Point(xc - r3, yc + r2));
                    polygon.Points.Add(new Point(xc - r3, yc - r2));
                    polygon.Stroke = Brushes.Black;
                    polygon.StrokeThickness = 1;

                    parent.AddChild(polygon);
                }
            }
        }

        public Cell Winner(double[] xs, bool isP)
        {
            double min = double.MaxValue;

            int minJ = 0;
            int minI = 0;

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (isP && map[i, j].P < Pmin()) continue;

                    double res = this.map[i, j].Neuron.D(xs);

                    if (min > res)
                    {
                        minI = i;
                        minJ = j;
                        min = res;
                    }
                }
            }

            return this.map[minI, minJ];
        }

        private double D(Polygon a, Polygon b)
        {
            Point pointA = a.Points[0];
            Point pointB = b.Points[0];

            return Math.Sqrt((pointA.X - pointB.X) * (pointA.X - pointB.X) + (pointA.Y - pointB.Y) * (pointA.Y - pointB.Y));
        }
        public List<Cell> Involved(Cell winner, double t, double T)
        {
            List<Cell> involved = new List<Cell>();

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int  j = 0; j < map.GetLength(1); j++)
                {
                    double d = D(winner.Polygon, map[i, j].Polygon);

                    if (d < SigmaFunction(t, T))
                    {
                        involved.Add(map[i,j]);
                    }
                }
            }

            return involved;
        }

        public double E(List<double[]> dataSet)
        {
            double sum = 0;
            int q = dataSet.Count;

            for (int i = 0; i < q; i++)
            {
                Cell winner = Winner(dataSet[i], false);
                double normaSq = 0;

                for (int j = 0; j < winner.Neuron.Ws.Count(); j++)
                {
                    normaSq += (winner.Neuron.Ws[j] - dataSet[i][j]) * (winner.Neuron.Ws[j] - dataSet[i][j]);
                }
                sum += normaSq;
            }

            return sum / q;
        }

        public void Recount(Cell winner, List<Cell> seconders, double[] xs, double t, double T)
        {
            for (int i = 0; i < seconders.Count; i++)
            {
                double n = N(D(winner.Polygon, seconders[i].Polygon), t, T);

                for (int j = 0; j < seconders[i].Neuron.Ws.Length; j++)
                {
                    double w = seconders[i].Neuron.Ws[j];
                    double x = xs[j];

                    seconders[i].Neuron.Ws[j] = w + (x - w) * n;
                }
            }
        }

        public double Pmin()
        {
            return 0.74;
        }

        public void P(Cell winner, int t)
        {
            int l = map.GetLength(0) * map.GetLength(1);

            if (t == 0)
            {
                for (int i = 0; i < map.GetLength(0); i++)
                {
                   for(int j = 0; j < map.GetLength(1); j++)
                    {
                        map[i, j].P = 1.0 / l;
                    }
                }
            }
            else
            {
                double min = Pmin();
               
                for (int i = 0; i < map.GetLength(0); i++)
                {
                    for (int j = 0; j < map.GetLength(1); j++)
                    {
                        if (map[i, j] == winner)
                        {
                            if (map[i, j].P > min) map[i, j].P = map[i, j].P - min;
                            else map[i, j].P = 0;
                        }
                        else
                        {
                            if (map[i, j].P < 1.0 - 1.0 / l) map[i, j].P = map[i, j].P + 1.0 / l;
                            else map[i, j].P = 0;
                        }
                    }
                } 
            }
        }

        public double N(double d, double t, double T)
        {
            return GaussFunction(d, t, T) * A(t, T);
        }

        public double A(double t, double T)
        {
            double sz = SigmaZero();
            return 0.3 * Math.Pow(Math.E, -Math.Log(sz) * (t / T));
        }
        public double GaussFunction(double d, double t, double T)
        {
            double s = SigmaFunction(t, T);
            return Math.Pow(Math.E, -(d*d / (2 * s*s)));
        }

        public double SigmaFunction(double t, double T)
        {
            double sz = SigmaZero();
            return sz * Math.Pow(Math.E, -Math.Log(sz) * (t/T));
        }

        public double SigmaZero()
        {
            return Math.Max(map.GetLength(0) * 2 * Cell.Size, map.GetLength(1) * Math.Sqrt(3) * Cell.Size) / 2;
        }
    }
}
