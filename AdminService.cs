using System;
using System.Collections.Generic;

namespace QuizProgram
{
    public class AdminService
    {
        private string password;
        private readonly QuizService quizService;

        public AdminService(QuizService service, string initialPassword)
        {
            quizService = service;
            password = initialPassword;
        }

        public bool Authenticate(string inputPassword)
        {
            return inputPassword == password;
        }

        public void ChangePassword(string newPassword)
        {
            password = newPassword;
        }

        public void AddNewQuestion()
        {
            Console.Write("Enter question text: ");
            string text = Console.ReadLine();

            List<string> options = new List<string>();
            for (int i = 0; i < 4; i++)
            {
                Console.Write($"Enter option {i + 1}: ");
                options.Add(Console.ReadLine());
            }

            Console.Write("Enter the number of the correct answer (1-4): ");
            if (int.TryParse(Console.ReadLine(), out int correctIndex) &&
                correctIndex >= 1 && correctIndex <= 4)
            {
                quizService.AddQuestion(new Question(text, options, correctIndex - 1));
                Console.WriteLine("Question added successfully!");
            }
            else
            {
                Console.WriteLine("Invalid input. Question not added.");
            }
        }

        public void RemoveQuestion()
        {
            var questions = quizService.GetAllQuestions();
            if (questions.Count == 0)
            {
                Console.WriteLine("No questions available to remove.");
                return;
            }

            for (int i = 0; i < questions.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {questions[i].Text}");
            }

            Console.Write("Enter the number of the question to remove: ");
            if (int.TryParse(Console.ReadLine(), out int questionNum) &&
                questionNum >= 1 && questionNum <= questions.Count)
            {
                quizService.RemoveQuestion(questionNum - 1);
                Console.WriteLine("Question removed successfully!");
            }
            else
            {
                Console.WriteLine("Invalid input. No question removed.");
            }
        }

        public void ViewAllQuestions()
        {
            var questions = quizService.GetAllQuestions();

            if (questions.Count == 0)
            {
                Console.WriteLine("No questions available.");
            }
            else
            {
                foreach (var question in questions)
                {
                    Console.WriteLine($"\nQuestion: {question.Text}");
                    for (int i = 0; i < question.Options.Count; i++)
                    {
                        string marker = (i == question.CorrectAnswerIndex) ? " (Correct)" : "";
                        Console.WriteLine($"  {i + 1}. {question.Options[i]}{marker}");
                    }
                }
            }
        }
    }
}