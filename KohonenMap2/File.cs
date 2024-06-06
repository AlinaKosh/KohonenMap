using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KohonenMap
{
    public class File
    {
        public DataSet Read(string path)
        {
            try
            {
                DataSet dataSet = new DataSet();

                using (StreamReader sr = new StreamReader(path))
                {
                    sr.ReadLine();
                    string line = sr.ReadLine();

                    while (!string.IsNullOrEmpty(line))
                    {
                        string[] strings = line.Split('\t');

                        dataSet.Add(
                            new InputData
                            (
                                 double.Parse(strings[0]),
                                 strings[1],
                                 strings[2],
                                 strings[3],
                                 strings[4], strings[5]
                            )
                        );

                        line = sr.ReadLine();
                    }
                }

                return dataSet;
            }
            catch
            {
                MessageBox.Show("Ошибка");
                return null;
            }
        }
    }
}
