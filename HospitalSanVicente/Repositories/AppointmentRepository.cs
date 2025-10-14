using System;
using System.Collections.Generic;
using System.Linq;
using HospitalSanVicente.Models;
using HospitalSanVicente.Data;

namespace HospitalSanVicente.Repositories;

// Repository to handle data operations related to appointments
public class AppointmentRepository
{
    private readonly Database _database;

    // Constructor receives the database instance
    public AppointmentRepository(Database database)
    {
        _database = database;
    }

    // Add a new appointment to the database
    public void Add(Appointment appointment)
    {
        _database.Appointments.Add(appointment);
    }

    // Update an existing appointment in the database
    public void Update(Appointment appointment)
    {
        var index = _database.Appointments.FindIndex(a => a.Id == appointment.Id);
        if (index != -1)
            _database.Appointments[index] = appointment;
    }

    // Retrieve an appointment by its unique ID
    public Appointment? GetById(Guid id)
    {
        return _database.Appointments.FirstOrDefault(a => a.Id == id);
    }

    // List all appointments for a specific patient, ordered by date/time
    public List<Appointment> GetByPatient(Guid patientId)
    {
        return _database.Appointments
            .Where(a => a.Patient.Id == patientId)
            .OrderBy(a => a.DateTime)
            .ToList();
    }

    // List all appointments for a specific doctor, ordered by date/time
    public List<Appointment> GetByDoctor(Guid doctorId)
    {
        return _database.Appointments
            .Where(a => a.Doctor.Id == doctorId)
            .OrderBy(a => a.DateTime)
            .ToList();
    }

    // Check if there is a scheduling conflict for a patient or doctor at the given date/time
    public bool HasConflict(Guid patientId, Guid doctorId, DateTime dateTime)
    {
        return _database.Appointments.Any(a =>
            a.DateTime == dateTime &&
            a.Status == AppointmentStatus.Scheduled &&
            (a.Patient.Id == patientId || a.Doctor.Id == doctorId));
    }
}
