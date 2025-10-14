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
                    PatientService.RegisterPatientFromConsole();
                    MainMenuView.Pause();
                    break;
                
                case "2":
                    PatientService.ListPatients();
                    MainMenuView.Pause();
                    break;
                
                case "3":
                    PatientService.SearchPatientByDocumentIdFromConsole();
                    MainMenuView.Pause();
                    break;
                
                case "4":
                    PatientService.UpdatePatientFromConsole();
                    MainMenuView.Pause();
                    break;
                
                case "5":
                    PatientService.DeletePatientFromConsole();
                    MainMenuView.Pause();
                    break;
                
                case "6":
                    Console.WriteLine("\nBack to main menu...");
                    exit = true;
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
