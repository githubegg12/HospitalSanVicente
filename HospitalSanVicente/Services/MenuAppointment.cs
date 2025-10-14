using HospitalSanVicente.Utils;

namespace HospitalSanVicente.Services;

public class MenuAppointment
{
    public static void Run()
    {
        bool exit = false;

        while (!exit)
        {
            MenuAppointmentView.ShowAppointmentMenu();
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    // Menu for doctor registration
                    MainMenuView.Pause();
                    break;
                
                case "2":
                    // Menu for List
                    MainMenuView.Pause();
                    break;
                
                case "3":
                    // Menu for  Update
                    MainMenuView.Pause();
                    break;
                 
                case "4":
                    exit = true;
                    Console.WriteLine("\nBack to main menu...");
                    MainMenuView.Pause();
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