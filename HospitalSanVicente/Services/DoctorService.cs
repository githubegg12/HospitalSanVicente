using ClinicaSalud.Services;
using HospitalSanVicente.Models;
using HospitalSanVicente.Repositories;
using HospitalSanVicente.Data;

namespace HospitalSanVicente.Services;

public class DoctorService
{
    private static Database _database = new Database();
    private static DoctorRepository _doctorRepository = new DoctorRepository(_database);

    // Method to register a new doctor (with validation)
    public static Doctor? RegisterDoctor(string firstName, string lastName, int documentId, string phoneNumber,
        string email, string specialty)
    {
        // Validate input parameters
        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) ||
            documentId <= 0 || string.IsNullOrWhiteSpace(phoneNumber) || string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(specialty))
        {
            Console.WriteLine("Invalid information");
            return null;
        }

        // Check if a doctor with the same DocumentID already exists
        var existingDoctor = _doctorRepository.GetByDocumentId(documentId);
        if (existingDoctor != null)
        {
            Console.WriteLine("Doctor with this DocumentID already exists");
            return null;
        }

        try
        {
            // Create a new Doctor object and register it
            Doctor newDoctor = new Doctor(firstName, lastName, documentId, phoneNumber, email, specialty);
            return _doctorRepository.Register(newDoctor);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error registering doctor: {ex.Message}");
            return null;
        }
    }
    
    // Method to register a doctor using console input and validation
    public static void RegisterDoctorFromConsole()
    {
        try
        {
            string firstName = InputValidator.ReadNonEmptyString("Enter first name");
            string lastName = InputValidator.ReadNonEmptyString("Enter last name");
            int documentId = InputValidator.ReadNonNegativeInt("Enter document ID");
            string phoneNumber = InputValidator.ReadAlphanumericString("Enter phone number");
            string email = InputValidator.ReadEmail("Enter email");
            string specialty = InputValidator.ReadNonEmptyString("Enter specialty");

            var doctor = RegisterDoctor(firstName, lastName, documentId, phoneNumber, email, specialty);

            if (doctor != null)
            {
                Console.WriteLine("Doctor registered successfully!");
                EmailService.SendDoctorRegistrationConfirmation(doctor);
            }
            else
            {
                Console.WriteLine("Failed to register doctor.");
            }
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Doctor registration canceled by user.");
        }
    }


    // Method to list all registered doctors with console output
    public static void ListDoctors()
    {
        try
        {
            List<Doctor> doctors = _doctorRepository.GetAll();

            if (doctors.Count == 0)
            {
                Console.WriteLine("No doctors registered.");
                return;
            }

            // Print header
            Console.WriteLine("\n--- Registered Doctors ---");
            Console.WriteLine(
                $"{"ID",-36} | {"First Name",-15} | {"Last Name",-15} | {"Document ID",-10} | {"Phone",-15} | {"Email",-25} | {"Specialty",-20}");
            Console.WriteLine(new string('-', 140));

            // Print each doctor data
            foreach (var d in doctors)
            {
                Console.WriteLine(
                    $"{d.Id,-36} | {d.FirstName,-15} | {d.LastName,-15} | {d.DocumentID,-10} | {d.PhoneNumber,-15} | {d.Email,-25} | {d.Specialty,-20}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving doctor list: {ex.Message}");
        }
    }
    
    // Method to search for a doctor by DocumentID from console input
    public static void SearchDoctorByDocumentIdFromConsole()
    {
        try
        {
            int documentId = InputValidator.ReadNonNegativeInt("Enter doctor DocumentID to search");
            var doctor = _doctorRepository.GetByDocumentId(documentId);

            if (doctor == null)
            {
                Console.WriteLine($"No doctor found with DocumentID: {documentId}");
                return;
            }

            // Print doctor details
            Console.WriteLine("\n--- Doctor Found ---");
            Console.WriteLine($"ID: {doctor.Id}");
            Console.WriteLine($"Name: {doctor.FirstName} {doctor.LastName}");
            Console.WriteLine($"Document ID: {doctor.DocumentID}");
            Console.WriteLine($"Phone: {doctor.PhoneNumber}");
            Console.WriteLine($"Email: {doctor.Email}");
            Console.WriteLine($"Specialty: {doctor.Specialty}");
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Doctor search canceled by user.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error searching doctor: {ex.Message}");
        }
    }

    // Method to update a doctor by DocumentID using console input and validation
    public static void UpdateDoctorFromConsole()
    {
        try
        {
            int documentId = InputValidator.ReadNonNegativeInt("Enter doctor DocumentID to update");
            var doctor = _doctorRepository.GetByDocumentId(documentId);

            if (doctor == null)
            {
                Console.WriteLine($"No doctor found with DocumentID: {documentId}");
                return;
            }

            Console.WriteLine($"Updating doctor: {doctor.FirstName} {doctor.LastName}");
            Console.WriteLine("Press Enter without typing to keep current value.");

            // Update First Name
            string? newFirstName = InputValidator.ReadOptionalValidatedString(
                $"Current First Name: {doctor.FirstName} | New First Name: ",
                input => !string.IsNullOrWhiteSpace(input) && input.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)),
                "Invalid name. Not updated."
            );
            if (!string.IsNullOrWhiteSpace(newFirstName))
                doctor.FirstName = newFirstName;

            // Update Last Name
            Console.Write($"Current Last Name: {doctor.LastName} | New Last Name: ");
            string? newLastName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newLastName))
                doctor.LastName = newLastName;

            // Update Document ID - make sure it's unique and positive
            Console.Write($"Current Document ID: {doctor.DocumentID} | New Document ID: ");
            string? newDocumentIdStr = Console.ReadLine();
            if (int.TryParse(newDocumentIdStr, out int newDocumentId) && newDocumentId > 0 &&
                newDocumentId != doctor.DocumentID)
            {
                // Check uniqueness
                var existing = _doctorRepository.GetByDocumentId(newDocumentId);
                if (existing == null)
                    doctor.DocumentID = newDocumentId;
                else
                    Console.WriteLine("Document ID already exists. Not updated.");
            }

            // Update Phone Number
            Console.Write($"Current Phone Number: {doctor.PhoneNumber} | New Phone Number: ");
            string? newPhone = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newPhone))
                doctor.PhoneNumber = newPhone;

            // Update Email
            string? newEmail = InputValidator.ReadOptionalValidatedString(
                $"Current Email: {doctor.Email} | New Email: ",
                input => System.Text.RegularExpressions.Regex.IsMatch(input, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"),
                "Invalid email format. Not updated."
            );
            if (!string.IsNullOrWhiteSpace(newEmail))
                doctor.Email = newEmail;

            // Update Specialty
            Console.Write($"Current Specialty: {doctor.Specialty} | New Specialty: ");
            string? newSpecialty = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newSpecialty))
                doctor.Specialty = newSpecialty;

            // Update repository using original documentId as key
            _doctorRepository.Update(documentId.ToString(), doctor);

            Console.WriteLine("Doctor updated successfully.");
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Doctor update canceled by user.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating doctor: {ex.Message}");
        }
    }
    // Method to delete a doctor by GUID ID using console input and confirmation
    public static void DeleteDoctorFromConsole()
    {
        try
        {
            Guid id = InputValidator.ReadGuid("Enter doctor GUID to delete");

            var doctor = _doctorRepository.GetById(id.ToString());

            if (doctor == null)
            {
                Console.WriteLine($"No doctor found with ID: {id}");
                return;
            }

            bool confirm = InputValidator.ReadYesOrNo($"Are you sure you want to delete doctor {doctor.FirstName} {doctor.LastName}? (Y/N)");

            if (confirm)
            {
                _doctorRepository.Delete(id.ToString());
                Console.WriteLine("Doctor deleted successfully.");
            }
            else
            {
                Console.WriteLine("Deletion canceled.");
            }
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Doctor deletion canceled by user.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting doctor: {ex.Message}");
        }
    }
}
