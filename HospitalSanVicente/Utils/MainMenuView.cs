namespace HospitalSanVicente.Utils;

public class MainMenuView
{
    public static void ShowMainMenu()
    {
        Console.Clear();
        Console.WriteLine("\n====================================");
        Console.WriteLine("         Hospital San Vicente");
        Console.WriteLine("====================================\n");

        Console.WriteLine("Hospital Services Menu:\n");
        Console.WriteLine("1. Patient Management");
        Console.WriteLine("2. Doctor Management");
        Console.WriteLine("3. Appointment Management");
        Console.WriteLine("4. Exit\n");

        Console.Write("Select an option (1-4): ");
    }

    public static void Pause()
    {
        Console.WriteLine("\nPress any key to continue...\n");
        Console.ReadKey();
    }
}