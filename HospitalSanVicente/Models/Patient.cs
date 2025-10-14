namespace HospitalSanVicente.Models;

public class Patient : Person
{
    private DateOnly _birthDate;
    private string _medicalRecordNumber;

    public Patient(string firstName, string lastName, int documentID, string phoneNumber, string email, DateOnly birthDate, string medicalRecordNumber)
        : base(firstName, lastName, documentID, phoneNumber, email)
    {
        BirthDate = birthDate;
        MedicalRecordNumber = medicalRecordNumber;
    }

    public DateOnly BirthDate
    {
        get => _birthDate;
        set
        {
            if (value <= DateOnly.FromDateTime(DateTime.Today))
                _birthDate = value;
        }
    }

    public string MedicalRecordNumber
    {
        get => _medicalRecordNumber;
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
                _medicalRecordNumber = value;
        }
    }

    // Read-only property that calculates the patient's age in years
    public int Age
    {
        get
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            int age = today.Year - _birthDate.Year;

            if (today < _birthDate.AddYears(age))
                age--;

            return age;
        }
    }
}