namespace HospitalSanVicente.Utils;

public class MenuAppointmentView
{
    public static void ShowAppointmentMenu()
    {
        Console.WriteLine("\n====================================");
        Console.WriteLine("         Hospital San Vicente");
        Console.WriteLine("====================================\n");
        Console.WriteLine("1. Create Appointment");
        Console.WriteLine("2. Appointment List");
        Console.WriteLine("3. Appointment Search");
        Console.WriteLine("4. Appointment Update");
        Console.WriteLine("5. Cancel Appointment");
        Console.WriteLine("6. Back to Main Menu\n");
    
        Console.Write("Select an option (1-10): ");
    }
}