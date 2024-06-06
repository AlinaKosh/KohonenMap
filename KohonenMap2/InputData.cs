using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KohonenMap
{
    /*
    public class InputData
    {
        double height;
        double leafLength;
        double leafWidth;
        double corollaLength;
        string flowerSize;
        string stemType;

        public InputData(double height, double leafLength, double leafWidth, double corollaLength, string flowerSize, string stemType)
        {
            this.height = height;
            this.leafLength = leafLength;
            this.leafWidth = leafWidth;
            this.corollaLength = corollaLength;
            this.flowerSize = flowerSize;
            this.stemType = stemType;
        }

        public double Height
        {
            get { return height; }
            set { height = value; }
        }

        public double LeafLength
        {
            get { return leafLength; }
            set { leafLength = value; }
        }

        public double LeafWidth
        {
            get { return leafWidth; }
            set { leafWidth = value; }
        }
        public double CorollaLength
        {
            get { return corollaLength; }
            set { corollaLength = value; }
        }
        public string FlowerSize
        {
            get { return flowerSize; }
            set { flowerSize = value; }
        }
        public string StemType
        {
            get { return stemType; }
            set { stemType = value; }
        }
    }
    */

    public class InputData
    {
        double balance; // баланс вкусовых характеристик
        string grapeQuality; // качество винограда
        string storage; // хранение
        string alcoholPercentage; // процент алкоголя
        string regionRating; // рейтинг региона
        string sugar; // сахар

        public InputData(double balance, string grapeQuality, string storage, string alcoholPercentage, string regionRating, string sugar)
        {
            this.balance = balance;
            this.grapeQuality = grapeQuality;
            this.storage = storage;
            this.alcoholPercentage = alcoholPercentage;
            this.regionRating = regionRating;
            this.sugar = sugar;
        }


        public double Balance
        {
            get { return balance; }
            set { balance = value; }
        }

        public string GrapeQuality
        {
            get { return grapeQuality; }
            set { grapeQuality = value; }
        }

        public string Storage
        {
            get { return storage; }
            set { storage = value; }
        }
        public string AlcoholPercentage
        {
            get { return alcoholPercentage; }
            set { alcoholPercentage = value; }
        }
        public string RegionRating
        {
            get { return regionRating; }
            set { regionRating = value; }
        }
        public string Sugar
        {
            get { return sugar; }
            set { sugar = value; }
        }
    }
}
