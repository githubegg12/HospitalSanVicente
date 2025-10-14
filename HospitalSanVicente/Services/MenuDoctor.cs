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
                    //Registration
                    DoctorService.RegisterDoctorFromConsole();
                    MainMenuView.Pause();

                    break;
                
                case "2":
                    //List
                    DoctorService.ListDoctors();
                    MainMenuView.Pause();
                    break;
                
                case "3":
                    //Search
                    DoctorService.SearchDoctorByDocumentIdFromConsole();
                    MainMenuView.Pause();
                    break;
                
                case "4":
                    //Update
                    DoctorService.UpdateDoctorFromConsole();
                    MainMenuView.Pause();
                    break;
                
                case "5":
                    //Delete
                    DoctorService.DeleteDoctorFromConsole();
                    MainMenuView.Pause();
                    break;
                 
                case "6":
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