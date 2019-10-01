using System;
using System.Collections.Generic;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {
            var book = new Book("Scott GradeBook");
            var done = false;

            while (!done)
            {
                System.Console.WriteLine("Enter the character 'q' to quit");
                var input = Console.ReadLine();
                if (input == "q")
                {
                    done = true;
                    continue;
                }

                try
                {
                    var grade = double.Parse(input);
                    book.AddGrade(grade);
                }
                catch (ArgumentException ex) //Exception ex: This will catch all type of exceptions
                {
                    System.Console.WriteLine(ex.Message);
                }
                catch(FormatException ex)
                {
                    System.Console.WriteLine(ex.Message);
                }
                finally //Piece of code that you always want to execute
                {
                    // ...
                }
            }

            var stats = book.GetStatistics();

            System.Console.WriteLine($"The lowest grade is {stats.Low}");
            System.Console.WriteLine($"The letter grade is {stats.Letter}");

        }
    }

}
