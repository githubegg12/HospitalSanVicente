using HospitalSanVicente.Utils;
namespace HospitalSanVicente.Services;

public class MainMenu
{
    public static void Run()
    {
        bool exit = false;

        while (!exit)
        {
            MainMenuView.ShowMainMenu();
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":// Menu for Patients registration/Update/List/Cancel
                    MenuPatientView.ShowPatientMenu();
                    MenuPatient.Run();
                    break;
                
                case "2":// Menu for Doctor registration/Update/List/Cancel
                MenuDoctorView.ShowDoctorMenu();
                MenuDoctor.Run();
                break;
                
                case "3":// Menu for appointments registration/Update/List/Cancel
                MenuAppointmentView.ShowAppointmentMenu();
                MenuAppointment.Run();
                    break;
                 
                case "4": //Exit
                    Console.WriteLine("\nExiting system...");
                    exit = true;
                    break;

                default:
                    Console.WriteLine("\nInvalid option. Try again.");
                    MainMenuView.Pause();
                    break;
            }
        }
    }

}