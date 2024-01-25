using GradeCalculator.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GradeCalculator.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Do you want to create a new input file? (yes/no):"); // when creating a file, please create it with .txt
            string createFile = Console.ReadLine().Trim().ToLower();

            string inputFile;
            if (createFile == "yes")
            {
                Console.WriteLine("Enter the name of the new input file:"); //when creating a file, please create it with.txt
                inputFile = Console.ReadLine();
                CreateInputFile(inputFile);
            }
            else
            {
                Console.WriteLine("Enter the name of the existing input file:"); //when creating a file, please create it with.txt
                inputFile = Console.ReadLine();
            }

            Console.WriteLine("Enter the name of the output file:"); //when creating a file, please create it with.txt
            string outputFile = Console.ReadLine();

            ProcessFiles(inputFile, outputFile);
        }

        static void CreateInputFile(string inputFile)
        {
            using (StreamWriter sw = new StreamWriter(inputFile))
            {
                Console.WriteLine("Enter student data in the format 'LastName, FirstName, Score1, Score2, ..., ScoreN'. Enter 'done' when finished.");
                while (true)
                {
                    string inputLine = Console.ReadLine();
                    if (inputLine.Trim().ToLower() == "done")
                    {
                        break;
                    }
                    sw.WriteLine(inputLine);
                }
            }
        }

        static void ProcessFiles(string inputFile, string outputFile)
        {
            List<Student> students = new List<Student>();

            // Read from the input file
            try
            {
                using (StreamReader sr = new StreamReader(inputFile))
                {
                    while (sr.Peek() >= 0)
                    {
                        string line = sr.ReadLine();
                        var data = line.Split(',');
                        string lastName = data[0].Trim();
                        string firstName = data[1].Trim();
                        var scores = data.Skip(2).Select(int.Parse).ToList();
                        students.Add(new Student(lastName, firstName, scores));
                    }
                }

                // Write to the output file
                using (StreamWriter sw = new StreamWriter(outputFile))
                {
                    foreach (var student in students)
                    {
                        sw.WriteLine($"{student.LastName}, {student.FirstName}, {string.Join(", ", student.QuizScores)}, Average: {student.AverageScore:0.##}, Grade: {student.LetterGrade}");
                    }
                }

                Console.WriteLine("Data loaded into output file."); // if you dont provide any path and simple a file name, it will be under project file -> bin\Debug\net6.0
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
            }
        }
    }
}
