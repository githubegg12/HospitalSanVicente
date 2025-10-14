using HospitalSanVicente.Utils;

namespace HospitalSanVicente.Services;

public class MenuDoctor
{
    public static void Run()
    {
        bool exit = false;

        while (!exit)
        {
            MenuDoctorView.ShowDoctorMenu();
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    // Menu for doctor registration
                    break;
                
                case "2":
                    // Menu for List
                    break;
                
                case "3":
                    // Menu for  Update
                    break;
                 
                case "4":
                    exit = true;
                    Console.WriteLine("\nBack to main menu...");
                    MainMenuView.ShowMainMenu();
                    break;

                default:
                    Console.WriteLine("\nInvalid option. Try again.");
                    MainMenuView.Pause();
                    break;
            }
        }
    }
}