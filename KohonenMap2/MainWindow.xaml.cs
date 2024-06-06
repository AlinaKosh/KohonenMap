using ConsoleApp7;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KohonenMap
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int Input = 6;
        KohonenNet net;
        KohonenMap map;
        File file = new File();
        ColorService colorService = new ColorService();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            Grid canvas = Map;

            canvas.Children.Clear();

            int m = int.Parse(M.Text);
            int n = int.Parse(N.Text);

            this.map = new KohonenMap(MainWindow.Input, n, m);
            this.map.InitMap(canvas);

            MessageBox.Show("Карта создана");
        }

        private void Learn_Click(object sender, RoutedEventArgs e)
        {
            
            List<double[]> data = file.Read(Path.Text).Normalize();

            int T = int.Parse(K.Text);
            int t = 0;
            int j;

            Cell winner = null;

            while (t < T)
            {
                j = (int) Math.Truncate(MyRandom.Rand() * data.Count);
                double[] xs = data[j];
                //j = (j + 1) % data.Count;

                //расставляем потенциалы
                map.P(winner, t);

                //определяем победителя с учетом потенциалов
                winner = map.Winner(xs, true);

                //определяем соседей
                List<Cell> change = map.Involved(winner, t, T);

                //переопределяем веса нейронов соседей 
                map.Recount(winner, change, xs, t, T);

                t++;
            }

            MessageBox.Show("Обучение карты Кохонена завершено");
        }

        private void Go_Click(object sender, RoutedEventArgs e)
        {
            int num = int.Parse(NetC.Text);
            int T = int.Parse(NetT.Text);

            this.net = new KohonenNet(MainWindow.Input, num);

            List<double[]> data = file.Read(NetPath.Text).Normalize();
            

            Neuron win = null;
            int t = 0;
            int j;

            double startE = this.net.E(data);

            

            while (t < T)
            {
                j = (int)Math.Truncate(MyRandom.Rand() * data.Count);
                double[] xs = data[j];
                //j = (j + 1) % data.Count;

                this.net.P(win, t);

                win = this.net.Winner(xs, true);

                List<Neuron> neurons = this.net.AllChangers(win, t, T);

                this.net.Recount(win, neurons, xs, t, T);

                t++;
            }

            string str = "Распределение по кластерам\n";
            double[] res = new double[num];


            for (int i = 0; i < data.Count; i++)
            {
                win = this.net.Winner(data[i], false);
                res[net.Neurons.IndexOf(win)]++;
            }

            for (int i = 0; i < res.Length; i++)
            {
                str += "Кластер №" + (i+1) + ": " + res[i] + " эл.\n";
            }

            MessageBox.Show("Обучение Сети Кохонена завершено\n" + str + "E(0): " + startE + "\nE(" + T + "): "+ this.net.E(data));
        }

        private void Res_Click(object sender, RoutedEventArgs e)
        {
            int m = int.Parse(M.Text);
            int n = int.Parse(N.Text);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    int p = int.Parse(P.Text);
                    map.Map[i, j].Polygon.Fill = new SolidColorBrush(colorService.GetColor(map.Map[i, j].Neuron.Ws[p]));
                }
            }
        }

        private void Cluster_Click(object sender, RoutedEventArgs e)
        {
            int m = int.Parse(M.Text);
            int n = int.Parse(N.Text);

            for (int i = 0; i < n; i++) 
            { 
                for (int j = 0; j < m; j++)
                {
                    Neuron curr = map.Map[i, j].Neuron;

                    double min = double.MaxValue;
                    int cluster = 1;

                    for (int k = 0; k < net.Neurons.Count; k++)
                    {
                        double d = curr.D(net.Neurons[k].Ws);

                        if (min > d)
                        {
                            min = d;
                            cluster = k + 1;
                        }
                    }

                    double p = 1.0 / cluster;

                    map.Map[i, j].Polygon.Fill = new SolidColorBrush(colorService.GetColor(p));
                }
            }
        }
    }
}
