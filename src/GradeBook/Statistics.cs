using System;

namespace GradeBook
{
    public class Statistics
    {
        public double Average
        {
            get
            {
                return Sum / Count;
            }
        }
        public double High;
        public double Low;
        public char Letter
        {
            get 
            {
                switch (Average)
                {
                    //Pattern matching: check in runtime the type of variable
                    case var d when d >= 90.0:
                        return 'A';
                    case var d when d >= 80.0:
                        return 'B';
                    case var d when d >= 70.0:
                        return 'C';
                    case var d when d >= 60.0:
                        return 'D';
                    default:
                        return 'F';
                }
            }
        }
        public double Sum;
        public int Count;
        public Statistics()
        {
            this.High = double.MinValue;
            this.Low = double.MaxValue;
            this.Sum = 0.0;
            this.Count = 0;
        }
        //Method to extact the Average
        public void Add(double number)
        {
            Sum += number;
            Count += 1;

            Low = Math.Min(number, Low);
            High = Math.Max(number, High);
        }
    }
}