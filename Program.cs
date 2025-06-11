using System;

namespace QuizProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            var repository = new QuizRepository();
            var quizService = new QuizService(repository);
            var adminService = new AdminService(quizService, "admin123");

            while (true)
            {
                Console.Clear();
                Console.WriteLine("WELCOME! to the Quiz Program!!!!");
                Console.WriteLine("1. Take Quiz");
                Console.WriteLine("2. Admin Login");
                Console.WriteLine("3. Exit");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        TakeQuiz(quizService);
                        break;
                    case "2":
                        AdminLogin(adminService, quizService);
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }
        static void AdminLogin(AdminService adminService, QuizService quizService)
        {
            Console.Clear();
            Console.WriteLine("hint: the default poassword is 'admin123' ");
            Console.Write("Enter admin password: ");
            string input = Console.ReadLine();

            if (adminService.Authenticate(input))
            {
                AdminMenu(adminService, quizService);
            }
            else
            {
                Console.WriteLine("Incorrect password. Press any key to continue...");
                Console.ReadKey();
            }
        }

        static void TakeQuiz(QuizService quizService)
        {
            if (quizService.GetAllQuestions().Count == 0)
            {
                Console.WriteLine("No questions available. Press any key to continue...");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            Console.WriteLine("Quiz Started!\n");

            int score = quizService.TakeQuiz();

            Console.WriteLine($"Quiz completed! Your score: {score}/{quizService.GetAllQuestions().Count}");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

       

        static void AdminMenu(AdminService adminService, QuizService quizService)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("ADMIN MENU");
                Console.WriteLine("1. Add Question");
                Console.WriteLine("2. Remove Question");
                Console.WriteLine("3. View All Questions");
                Console.WriteLine("4. Change Admin Password");
                Console.WriteLine("5. Back to Main Menu");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        adminService.AddNewQuestion();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                    case "2":
                        Console.Clear();
                        adminService.RemoveQuestion();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                    case "3":
                        Console.Clear();
                        adminService.ViewAllQuestions();
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        break;
                    case "4":
                        ChangePassword(adminService);
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void ChangePassword(AdminService adminService)
        {
            Console.Clear();
            Console.WriteLine("CHANGE ADMIN PASSWORD");

            Console.Write("Enter new password: ");
            string newPass1 = Console.ReadLine();

            Console.Write("Confirm new password: ");
            string newPass2 = Console.ReadLine();

            if (newPass1 == newPass2)
            {
                adminService.ChangePassword(newPass1);
                Console.WriteLine("Password changed successfully!");
            }
            else
            {
                Console.WriteLine("Passwords don't match. Password not changed.");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}