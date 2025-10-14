using System;

namespace HospitalSanVicente.Models;

// Enum to represent the status of an appointment
public enum AppointmentStatus
{
    Scheduled,
    Cancelled,
    Attended
}

public class Appointment
{
    public Guid Id { get; private set; }
    public Patient Patient { get; private set; }    
    public Doctor Doctor { get; private set; }      
    public DateTime DateTime { get; private set; }  
    public AppointmentStatus Status { get; private set; } // Current status of the appointment

    // Constructor: initializes a new appointment with status Scheduled
    public Appointment(Patient patient, Doctor doctor, DateTime dateTime)
    {
        Id = Guid.NewGuid();
        Patient = patient;
        Doctor = doctor;
        DateTime = dateTime;
        Status = AppointmentStatus.Scheduled;
    }

    // Method to cancel the appointment, setting its status accordingly
    public void Cancel()
    {
        Status = AppointmentStatus.Cancelled;
    }

    // Method to mark the appointment as attended
    public void MarkAsAttended()
    {
        Status = AppointmentStatus.Attended;
    }
}