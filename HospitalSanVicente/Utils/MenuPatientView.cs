namespace HospitalSanVicente.Utils;

public class MenuPatientView
{
    public static void ShowPatientMenu()
    {
        Console.WriteLine("====================================");
        Console.WriteLine("         Hospital San Vicente");
        Console.WriteLine("====================================\n");
        Console.WriteLine("1. Patient Registration");
        Console.WriteLine("2. Patient List");
        Console.WriteLine("3. Patient Search");
        Console.WriteLine("4. Patient Update");
        Console.WriteLine("5. Delete Patient");
        Console.WriteLine("6. Back to Main Menu\n");
        
        Console.Write("Select an option (1-6): ");
    }
}