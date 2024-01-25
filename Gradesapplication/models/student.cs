using System.Collections.Generic;
using System.Linq;

namespace GradeCalculator.Models
{
    public class Student
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public List<int> QuizScores { get; set; }
        public double AverageScore { get; set; }
        public char LetterGrade { get; set; }

        public Student(string lastName, string firstName, List<int> quizScores)
        {
            LastName = lastName;
            FirstName = firstName;
            QuizScores = quizScores;
            AverageScore = QuizScores.Sum() / 10.0; // even if fewer than 10 quizzes, the denominator is 10
            LetterGrade = CalculateLetterGrade(AverageScore);
        }

        private char CalculateLetterGrade(double averageScore)
        {
            if (averageScore >= 90) return 'A';
            if (averageScore >= 80) return 'B';
            if (averageScore >= 70) return 'C';
            if (averageScore >= 60) return 'D';
            return 'F';
        }
    }
}
