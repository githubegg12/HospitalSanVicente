using HospitalSanVicente.Data;
using HospitalSanVicente.Interfaces;
using HospitalSanVicente.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalSanVicente.Repositories
{
    public class PatientRepository : ICreate<Patient>, IRead<Patient>, IUpdate<Patient>, IDelete
    {
        private readonly Database _database;

        // Constructor injects the Database instance
        public PatientRepository(Database database)
        {
            _database = database;
        }

        // Register a new patient
        public Patient Register(Patient patient)
        {
            _database.Patients.Add(patient);
            return patient;
        }

        // Get all patients
        public List<Patient> GetAll()
        {
            return _database.Patients;
        }

        // Get patient by Guid id (string)
        public Patient? GetById(string id)
        {
            if (!Guid.TryParse(id, out var guid))
                return null;

            return _database.Patients.FirstOrDefault(p => p.Id == guid);
        }

        // Update patient info by id
        public void Update(string id, Patient patient)
        {
            if (!Guid.TryParse(id, out var guid))
                return;

            var index = _database.Patients.FindIndex(p => p.Id == guid);
            if (index != -1)
            {
                _database.Patients[index] = patient;
            }
        }

        // Remove patient by id
        public void Delete(string id)
        {
            if (!Guid.TryParse(id, out var guid))
                return;

            _database.Patients.RemoveAll(p => p.Id == guid);
        }

        // Get patient by DocumentID (int)
        public Patient? GetByDocumentId(int documentId)
        {
            return _database.Patients.FirstOrDefault(p => p.DocumentID == documentId);
        }
    }
}