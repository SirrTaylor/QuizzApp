using System;
using System.Collections.Generic;

namespace QuizProgram
{
    public class QuizService
    {
        private List<Question> questions;
        private readonly QuizRepository repository;

        public QuizService(QuizRepository repo)
        {
            repository = repo;
            questions = repository.LoadQuestions();
        }

        public void AddQuestion(Question question)
        {
            questions.Add(question);
            repository.SaveQuestions(questions);
        }

        public void RemoveQuestion(int index)
        {
            if (index >= 0 && index < questions.Count)
            {
                questions.RemoveAt(index);
                repository.SaveQuestions(questions);
            }
        }

        public List<Question> GetAllQuestions()
        {
            return new List<Question>(questions);
        }

        public int TakeQuiz()
        {
            int score = 0;

            foreach (var question in questions)
            {
                Console.WriteLine(question.Text);
                for (int i = 0; i < question.Options.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {question.Options[i]}");
                }

                Console.Write("Your answer (1-4): ");
                if (int.TryParse(Console.ReadLine(), out int userAnswer) &&
                    userAnswer >= 1 && userAnswer <= 4)
                {
                    if (userAnswer - 1 == question.CorrectAnswerIndex)
                    {
                        Console.WriteLine("Correct!\n");
                        score++;
                    }
                    else
                    {
                        Console.WriteLine($"Incorrect! The correct answer was: {question.CorrectAnswerIndex + 1}\n");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Skipping question.\n");
                }
            }

            return score;
        }
    }
}