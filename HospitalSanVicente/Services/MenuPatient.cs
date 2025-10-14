using HospitalSanVicente.Utils;

namespace HospitalSanVicente.Services;

public class MenuPatient
{
    public static void Run()
    {
        bool exit = false;

        while (!exit)
        {
            MenuPatientView.ShowPatientMenu();
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    // Menu for patient registration
                    break;
                
                case "2":
                    // Menu for List
                    break;
                
                case "3":
                    // Menu for  Update
                    break;
                
                case "4":
                    Console.WriteLine("\nBack to main menu...");
                    exit = true;
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
