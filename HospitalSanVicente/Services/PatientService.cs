
using ClinicaSalud.Services;
using HospitalSanVicente.Models;
using HospitalSanVicente.Repositories;
using HospitalSanVicente.Data;

namespace HospitalSanVicente.Services;

public class PatientService
{
    private static Database _database = new Database();
    private static PatientRepository _patientRepository = new PatientRepository(_database);

    // Existing method to register patient with parameters (no console)
    public static Patient? RegisterPatient(string firstName, string lastName, int documentId, string phoneNumber,
        string email, DateOnly birthDate)
    {
        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) ||
            documentId <= 0 || string.IsNullOrWhiteSpace(phoneNumber) || string.IsNullOrWhiteSpace(email))
        {
            Console.WriteLine("Invalid information");
            return null;
        }

        var existingPatient = _patientRepository.GetByDocumentId(documentId);
        if (existingPatient != null)
        {
            Console.WriteLine("Patient with this DocumentID already exists");
            return null;
        }

        try
        {
            Patient newPatient = new Patient(firstName, lastName, documentId, phoneNumber, email, birthDate);
            return _patientRepository.Register(newPatient);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error registering patient: {ex.Message}");
            return null;
        }
    }

    // Method to register patient using console input and validation
    public static void RegisterPatientFromConsole()
    {
        try
        {
            string firstName = InputValidator.ReadNonEmptyString("Enter first name");
            string lastName = InputValidator.ReadNonEmptyString("Enter last name");
            int documentId = InputValidator.ReadNonNegativeInt("Enter document ID");
            string phoneNumber = InputValidator.ReadAlphanumericString("Enter phone number");
            string email = InputValidator.ReadEmail("Enter email");

            DateTime birthDateTime = InputValidator.ReadDateTime("Enter birth date (yyyy-MM-dd)");
            DateOnly birthDate = DateOnly.FromDateTime(birthDateTime);


            var patient = RegisterPatient(firstName, lastName, documentId, phoneNumber, email, birthDate);

            if (patient != null)
            {
                Console.WriteLine("Patient registered successfully!");
            }
            else
            {
                Console.WriteLine("Failed to register patient.");
            }
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Patient registration canceled by user.");
        }
    }

    // Method to list all patients with console output
    public static void ListPatients()
    {
        try
        {
            List<Patient> patients = _patientRepository.GetAll();

            if (patients.Count == 0)
            {
                Console.WriteLine("No patients registered.");
                return;
            }

            // Print header
            Console.WriteLine("\n--- Registered Patients ---");
            Console.WriteLine(
                $"{"ID",-36} | {"First Name",-15} | {"Last Name",-15} | {"Document ID",-10} | {"Phone",-15} | {"Email",-25} | {"Birth Date",-12} ");
            Console.WriteLine(new string('-', 140));

            // Print each patient data
            foreach (var p in patients)
            {
                Console.WriteLine(
                    $"{p.Id,-36} | {p.FirstName,-15} | {p.LastName,-15} | {p.DocumentID,-10} | {p.PhoneNumber,-15} | {p.Email,-25} | {p.BirthDate.ToString("yyyy-MM-dd"),-12} ");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving patient list: {ex.Message}");
        }
    }

    // Method to search for patient by document ID from console input
    public static void SearchPatientByDocumentIdFromConsole()
    {
        try
        {
            int documentId = InputValidator.ReadNonNegativeInt("Enter document ID to search");
            var patient = _patientRepository.GetByDocumentId(documentId);

            if (patient == null)
            {
                Console.WriteLine($"No patient found with Document ID: {documentId}");
                return;
            }

            // Print patient details
            Console.WriteLine("\n--- Patient Found ---");
            Console.WriteLine($"ID: {patient.Id}");
            Console.WriteLine($"Name: {patient.FirstName} {patient.LastName}");
            Console.WriteLine($"Document ID: {patient.DocumentID}");
            Console.WriteLine($"Phone: {patient.PhoneNumber}");
            Console.WriteLine($"Email: {patient.Email}");
            Console.WriteLine($"Birth Date: {patient.BirthDate:yyyy-MM-dd}");
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Patient search canceled by user.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error searching patient: {ex.Message}");
        }
    }

    // Method to update a patient by ID using console input and validation
    // Method to update a patient by DocumentID using console input and validation
    public static void UpdatePatientFromConsole()
    {
        try
        {
            int documentId = InputValidator.ReadNonNegativeInt("Enter patient DocumentID to update");
            var patient = _patientRepository.GetByDocumentId(documentId);

            if (patient == null)
            {
                Console.WriteLine($"No patient found with DocumentID: {documentId}");
                return;
            }

            Console.WriteLine($"Updating patient: {patient.FirstName} {patient.LastName}");
            Console.WriteLine("Press Enter without typing to keep current value.");

            // Read and update First Name (optional)
            string? newFirstName = InputValidator.ReadOptionalValidatedString(
                $"Current First Name: {patient.FirstName} | New First Name: ",
                input => !string.IsNullOrWhiteSpace(input) && input.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)),
                "Invalid name. Not updated."
            );
            if (!string.IsNullOrWhiteSpace(newFirstName))
                patient.FirstName = newFirstName;

            // Last Name
            Console.Write($"Current Last Name: {patient.LastName} | New Last Name: ");
            string? newLastName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newLastName))
                patient.LastName = newLastName;

            // Document ID - optional update, but make sure it's unique and positive
            Console.Write($"Current Document ID: {patient.DocumentID} | New Document ID: ");
            string? newDocumentIdStr = Console.ReadLine();
            if (int.TryParse(newDocumentIdStr, out int newDocumentId) && newDocumentId > 0 &&
                newDocumentId != patient.DocumentID)
            {
                // Check uniqueness
                var existing = _patientRepository.GetByDocumentId(newDocumentId);
                if (existing == null)
                    patient.DocumentID = newDocumentId;
                else
                    Console.WriteLine("Document ID already exists. Not updated.");
            }

            // Phone Number
            Console.Write($"Current Phone Number: {patient.PhoneNumber} | New Phone Number: ");
            string? newPhone = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newPhone))
                patient.PhoneNumber = newPhone;

            // Email
            string? newEmail = InputValidator.ReadOptionalValidatedString(
                $"Current Email: {patient.Email} | New Email: ",
                input => System.Text.RegularExpressions.Regex.IsMatch(input, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"),
                "Invalid email format. Not updated."
            );
            if (!string.IsNullOrWhiteSpace(newEmail))
                patient.Email = newEmail;

            // Birth Date
            Console.Write($"Current Birth Date: {patient.BirthDate:yyyy-MM-dd} | New Birth Date (yyyy-MM-dd): ");
            string? newBirthDateStr = Console.ReadLine();
            if (DateTime.TryParse(newBirthDateStr, out DateTime newBirthDateTime))
                patient.BirthDate = DateOnly.FromDateTime(newBirthDateTime);

            // Update repository using the original documentId as key, assuming repository Update uses DocumentID as key
            _patientRepository.Update(documentId.ToString(), patient);

            Console.WriteLine("Patient updated successfully.");
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Patient update canceled by user.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating patient: {ex.Message}");
        }
    }


    // Method to delete a patient by ID using console input and confirmation
    public static void DeletePatientFromConsole()
    {
        try
        {
            Guid id = InputValidator.ReadGuid("Enter patient GUID to delete");

            var patient = _patientRepository.GetById(id.ToString());

            if (patient == null)
            {
                Console.WriteLine($"No patient found with ID: {id}");
                return;
            }

            bool confirm = InputValidator.ReadYesOrNo($"Are you sure you want to delete patient {patient.FirstName} {patient.LastName}? (Y/N)");

            if (confirm)
            {
                _patientRepository.Delete(id.ToString());
                Console.WriteLine("Patient deleted successfully.");
            }
            else
            {
                Console.WriteLine("Deletion canceled.");
            }
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Patient deletion canceled by user.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting patient: {ex.Message}");
        }
    }
}

