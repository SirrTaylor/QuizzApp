using System;
using System.Collections.Generic;
using System.IO;

namespace QuizProgram
{
    public class QuizRepository
    {
        private const string QuestionsFile = "questions.txt";

        public virtual List<Question> LoadQuestions()
        {
            List<Question> questions = new List<Question>();

            if (File.Exists(QuestionsFile))
            {
                try
                {
                    string[] lines = File.ReadAllLines(QuestionsFile);
                    foreach (string line in lines)
                    {
                        if (string.IsNullOrWhiteSpace(line)) continue;

                        string[] parts = line.Split('|');
                        if (parts.Length >= 6)
                        {
                            string text = parts[0];
                            List<string> options = new List<string> { parts[1], parts[2], parts[3], parts[4] };
                            int correctIndex = int.Parse(parts[5]);
                            questions.Add(new Question(text, options, correctIndex));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading questions: {ex.Message}");
                }
            }

            return questions;
        }

        public virtual void SaveQuestions(List<Question> questions)
        {
            try
            {
                List<string> lines = new List<string>();
                foreach (var question in questions)
                {
                    string line = $"{question.Text}|{question.Options[0]}|{question.Options[1]}|" +
                                $"{question.Options[2]}|{question.Options[3]}|{question.CorrectAnswerIndex}";
                    lines.Add(line);
                }
                File.WriteAllLines(QuestionsFile, lines);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving questions: {ex.Message}");
            }
        }
    }
}