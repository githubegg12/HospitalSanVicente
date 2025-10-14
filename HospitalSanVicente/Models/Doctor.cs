namespace HospitalSanVicente.Models;

public class Doctor : Person
{
    private string _specialty;

    // Constructor
    public Doctor(string firstName, string lastName, int documentID, string phoneNumber, string email, string specialty)
        : base(firstName, lastName, documentID, phoneNumber, email)
    {
        Specialty = specialty;
    }
    public string Specialty
    {
        get => _specialty;
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
                _specialty = value;
        }
    }
}