using System;
using System.Collections.Generic;
using System.IO;

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
    public abstract class Book: NamedObject, IBook
    {
        public Book(string name): base(name)
        {

        }

        public abstract event GradeAddedDelegate GradeAdded;
        public abstract void AddGrade(double grade); //I do not provide the implementation, the derived clases do
        public abstract Statistics GetStatistics(); //A derived class will choose to override this implementation

    }
    public interface IBook
    {
        void AddGrade(double grade);
        Statistics GetStatistics();
        string Name { get; }
        event GradeAddedDelegate GradeAdded;
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

        public override event GradeAddedDelegate GradeAdded; //event: Makes the delegate safer to use, only events can be added to is

        public override Statistics GetStatistics()
        {
            var result = new Statistics();

            foreach (var grade in grades)
            {
                result.Add(grade);
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
    public class DiskBook : Book
    {
        public DiskBook(string name) : base(name)
        {
        }

        public override event GradeAddedDelegate GradeAdded;

        public override void AddGrade(double grade)
        {
            //Compiler creates a try catch and call a dispose when the resource is not used anymore
            using(var writer = File.AppendText($"{Name}.txt"))
            {
                writer.WriteLine(grade);
                if (GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }
            }

        }

        public override Statistics GetStatistics()
        {
            var result = new Statistics();

            using(var reader = File.OpenText( $"{Name}.txt"))
            {
                var line = reader.ReadLine();
                while(line != null)
                {
                    var number = Double.Parse(line);
                    result.Add(number);
                }
            }

            return result;
        }
    }
}