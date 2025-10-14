using HospitalSanVicente.Data;
using HospitalSanVicente.Services;
using HospitalSanVicente.Utils;

namespace HospitalSanVicente.Services;

public class MenuAppointment
{
    public static void Run()
    {
        bool exit = false;

        // Create an instance of AppointmentService to use its non-static methods
        var database = new Database();
        var _appointmentService = new AppointmentService(database);

        while (!exit)
        {
            MenuAppointmentView.ShowAppointmentMenu();
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    // Call method to schedule an appointment
                    _appointmentService.ScheduleAppointmentFromConsole();
                    MainMenuView.Pause();
                    break;

                case "2":
                    // List appointments by patient
                    _appointmentService.ListAppointmentsByPatientFromConsole();
                    MainMenuView.Pause();
                    break;

                case "3":
                    // List appointments by doctor
                    _appointmentService.ListAppointmentsByDoctorFromConsole();
                    MainMenuView.Pause();
                    break;

                case "4":
                    // Cancel an appointment
                    _appointmentService.CancelAppointmentFromConsole();
                    MainMenuView.Pause();
                    break;

                case "5":
                    // Mark an appointment as attended
                    _appointmentService.MarkAppointmentAttendedFromConsole();
                    MainMenuView.Pause();
                    break;

                case "6":
                    // Exit to main menu
                    exit = true;
                    Console.WriteLine("\nBack to main menu...");
                    MainMenuView.Pause();
                    MainMenuView.ShowMainMenu();
                    break;

                default:
                    // Handle invalid menu option
                    Console.WriteLine("\nInvalid option. Try again.");
                    MainMenuView.Pause();
                    break;
            }
        }
    }
}
