using HospitalSanVicente.Models;
using HospitalSanVicente.Repositories;
using System;
using System.Collections.Generic;
using HospitalSanVicente.Data;

namespace HospitalSanVicente.Services
{
    public class PatientService
    {
        private static Database _database = new Database(); // Or inject this properly
        private static PatientRepository _patientRepository = new PatientRepository(_database);

        public static Patient? RegisterPatient(string firstName, string lastName, int documentId, string phoneNumber, string email, DateOnly birthDate, string medicalRecordNumber)
        {
            // Basic validation
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) ||
                documentId <= 0 || string.IsNullOrWhiteSpace(phoneNumber) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(medicalRecordNumber))
            {
                Console.WriteLine("Invalid information");
                return null;
            }

            // Validate unique document ID
            var existingPatient = _patientRepository.GetByDocumentId(documentId);
            if (existingPatient != null)
            {
                Console.WriteLine("Patient with this DocumentID already exists");
                return null;
            }

            try
            {
                Patient newPatient = new Patient(firstName, lastName, documentId, phoneNumber, email, birthDate, medicalRecordNumber);
                return _patientRepository.Register(newPatient);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error registering patient: {ex.Message}");
                return null;
            }
        }

        public static List<Patient> GetAllPatients()
        {
            try
            {
                return _patientRepository.GetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting patients: {ex.Message}");
                return new List<Patient>();
            }
        }

        public static Patient? GetPatientById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                Console.WriteLine("Invalid Id");
                return null;
            }

            try
            {
                return _patientRepository.GetById(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting patient by id: {ex.Message}");
                return null;
            }
        }

        public static void UpdatePatient(string id, string firstName, string lastName, int documentId, string phoneNumber, string email, DateOnly birthDate, string medicalRecordNumber)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                Console.WriteLine("Invalid Id");
                return;
            }

            try
            {
                Patient patientToUpdate = new Patient(firstName, lastName, documentId, phoneNumber, email, birthDate, medicalRecordNumber);
                _patientRepository.Update(id, patientToUpdate);
                Console.WriteLine("Patient updated successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating patient: {ex.Message}");
            }
        }

        public static void RemovePatient(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                Console.WriteLine("Invalid Id");
                return;
            }

            try
            {
                _patientRepository.Delete(id);
                Console.WriteLine("Patient removed successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing patient: {ex.Message}");
            }
        }
    }
}
