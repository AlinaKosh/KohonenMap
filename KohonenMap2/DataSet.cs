using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KohonenMap
{
    public class DataSet
    {
        List<InputData> dataSet;

        public DataSet()
        {
            this.dataSet = new List<InputData>();
        }

        public DataSet(List<InputData> dataSet)
        {
            this.dataSet = dataSet;
        }

        public void Add(InputData data)
        {
            dataSet.Add(data);  
        }

        public InputData Get(int i)
        {
            return dataSet[i];
        }

        public List<double[]> Normalize()
        {
            double min1 = dataSet.Min(s => s.Balance);
            double max1 = dataSet.Max(s => s.Balance);

            List<double[]> res = new List<double[]>();

            for (int i = 0; i < dataSet.Count; i++)
            {
                double[] data = new double[MainWindow.Input];

                data[0] = (dataSet[i].Balance - min1) / (max1 - min1);

                switch (dataSet[i].GrapeQuality)
                {
                    case "высокое": data[1] = 0; break;
                    case "низкое": data[1] = 1; break;
                }

                switch (dataSet[i].Storage)
                {
                    case "бутылка": data[2] = 0; break;
                    case "бочка": data[2] = 1; break;
                }

                switch (dataSet[i].AlcoholPercentage)
                {
                    case "крепленое": data[3] = 0; break;
                    case "некрепленое": data[3] = 1; break;
                }

                switch (dataSet[i].RegionRating)
                {
                    case "низкий": data[4] = 0; break;
                    case "высокий": data[4] = 1; break;
                }

                switch (dataSet[i].Sugar)
                {
                    case "нет": data[5] = 0; break;
                    case "да": data[5] = 1; break;
                }

                res.Add(data);
            }

            return res;
        }
    }
}
