using System;
using System.Collections.Generic;

namespace GradeBook
{
    public delegate void GradeAddedDelegate(object sender, EventArgs args);
    public class NamedObject //Inheretance name property
    {
        public NamedObject(string name)
        {
            this.Name = name;

        }
        public string Name { get; set; }
    }
    public abstract class Book: NamedObject
    {
        public Book(string name): base(name)
        {

        }
        public abstract void AddGrade(double grade); //I do not provide the implementation, the derived clases do
    }

    public class InMemoryBook : Book
    {
        //Base: accessing parent class constructor
        public InMemoryBook(string name): base(name)
        {
            grades = new List<double>();
        }
        //Method overload: has a different signature (name + parameter)
        public void AddGrade(char letter)
        {
            switch (letter)
            {
                case 'A':
                    AddGrade(90);
                    break;
                case 'B':
                    AddGrade(80);
                    break;
                case 'C':
                    AddGrade(70);
                    break;
                case 'D':
                    AddGrade(60);
                    break;
                default:
                    AddGrade(0);
                    break;
            }
        }
        public override void AddGrade(double grade)
        {
            if (grade <= 100 && grade >= 0)
            {
                grades.Add(grade);
                if (GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }

                // Event to let know when we add a grade
            }
            else
            {
                throw new ArgumentException($"Invalid {nameof(grade)}");
            }
        }

        public event GradeAddedDelegate GradeAdded; //event: Makes the delegate safer to use, only events can be added to is

        public Statistics GetStatistics()
        {
            var result = new Statistics();
            result.Average = 0.0;
            result.High = double.MinValue;
            result.Low = double.MaxValue;

            foreach (var grade in grades)
            {
                if (grade == 42.1)
                {
                    //break;
                    //continue;
                }

                result.Low = Math.Min(grade, result.Low);
                result.High = Math.Max(grade, result.High);
                result.Average += grade;
            }

            result.Average /= grades.Count;

            switch (result.Average)
            {
                //Pattern matching: check in runtime the type of variable
                case var d when d >= 90.0:
                    result.Letter = 'A';
                    break;
                case var d when d >= 80.0:
                    result.Letter = 'B';
                    break;
                case var d when d >= 70.0:
                    result.Letter = 'C';
                    break;
                case var d when d >= 60.0:
                    result.Letter = 'D';
                    break;
                default:
                    result.Letter = 'F';
                    break;
            }

            return result;
        }

        private List<double> grades;
        // public string Name 
        // { 
        //     get
        //     {
        //         return name;
        //     }
        //     set
        //     {
        //         if (!String.IsNullOrEmpty(value))
        //         {
        //             name = value;
        //         }
        //     } 
        // }
        // private string name; 
        readonly string category = "Science"; //Initialize only in the constructor or variable initializer
        public const string OTHERCATEGORY = "Math"; //Const with uppercase
    }
}