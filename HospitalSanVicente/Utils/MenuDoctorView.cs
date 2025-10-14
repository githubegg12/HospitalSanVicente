namespace HospitalSanVicente.Utils;

public class MenuDoctorView
{
    public static void ShowDoctorMenu()
    {
        Console.WriteLine("====================================");
        Console.WriteLine("         Hospital San Vicente");
        Console.WriteLine("====================================\n");
        Console.WriteLine("1. Doctor Registration");
        Console.WriteLine("2. Doctor List");
        Console.WriteLine("3. Doctor Search");
        Console.WriteLine("4. Doctor Update");
        Console.WriteLine("5. Delete Doctor");
        Console.WriteLine("6. Back to Main Menu\n");
    
        Console.Write("Select an option (1-6): ");
    }
}