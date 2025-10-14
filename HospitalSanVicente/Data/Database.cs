namespace HospitalSanVicente.Data;

using Models;

public class Database
{
    public List<Patient> Patients { get; set; }
    public List<Doctor> Doctors { get; set; }
    public List<Appointment> Appointments { get; set; }
    
    public List<EmailLog> EmailLogs { get; set; } = new ();

    public Database()
    {
        Patients = new List<Patient>()
        {
            new Patient("Juan", "Perez", 12345678, "555-1234", "juan.perez@example.com", DateOnly.Parse("1985-05-15")),
            new Patient("Ana", "Gomez", 87654321, "555-5678", "ana.gomez@example.com", DateOnly.Parse("1990-10-20")),
            new Patient("Luis", "Martinez", 11223344, "555-8765", "luis.martinez@example.com", DateOnly.Parse("1978-02-28"))
        };

        Doctors = new List<Doctor>();
        Appointments = new List<Appointment>();
    }
}
