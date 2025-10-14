using HospitalSanVicente.Data;
using HospitalSanVicente.Interfaces;
using HospitalSanVicente.Models;

namespace HospitalSanVicente.Repositories;

public class DoctorRepository : ICreate<Doctor>, IRead<Doctor>, IUpdate<Doctor>, IDelete
{
    private readonly Database _database;

    // Constructor injects the Database instance
    public DoctorRepository(Database database)
    {
        _database = database;
    }

    // Register a new doctor
    public Doctor Register(Doctor doctor)
    {
        _database.Doctors.Add(doctor);
        return doctor;
    }

    // Get all doctors
    public List<Doctor> GetAll()
    {
        return _database.Doctors;
    }

    // Get doctor by GUID id (string)
    public Doctor? GetById(string id)
    {
        if (!Guid.TryParse(id, out var guid))
            return null;

        return _database.Doctors.FirstOrDefault(d => d.Id == guid);
    }

    // Update doctor info by GUID id
    public void Update(string id, Doctor doctor)
    {
        if (!Guid.TryParse(id, out var guid))
            return;

        var index = _database.Doctors.FindIndex(d => d.Id == guid);
        if (index != -1)
        {
            _database.Doctors[index] = doctor;
        }
    }

    // Remove doctor by GUID id
    public void Delete(string id)
    {
        if (!Guid.TryParse(id, out var guid))
            return;

        _database.Doctors.RemoveAll(d => d.Id == guid);
    }

    // Get doctor by DocumentID (int)
    public Doctor? GetByDocumentId(int documentId)
    {
        return _database.Doctors.FirstOrDefault(d => d.DocumentID == documentId);
    }
}