using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KohonenMap
{
    public class Map
    {
        Cell[,] map;

        public Map() {
            
        }

        public Map(Cell[,] map)
        {
            this.map = map;
        }

        public List<Neuron> Neurons { 
            get 
            {  
                List<Neuron> list = new List<Neuron>();
                for (int i = 0; i < map.GetLength(0); i++)
                {
                    for (int j = 0; j < map.GetLength(1); j++)
                    {
                        list.Add(map[i, j].Neuron);
                    }
                }
                return list;
            } 
        }
    }
}
