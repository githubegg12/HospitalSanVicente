namespace HospitalSanVicente.Models;

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
    public DateTime AppointmentDateTime { get; private set; }
    public AppointmentStatus Status { get; private set; }
    
    public Appointment(Patient patient, Doctor doctor, DateTime appointmentDateTime)
    {
        Id = Guid.NewGuid();
        Patient = patient;
        Doctor = doctor;
        AppointmentDateTime = appointmentDateTime;
        Status = AppointmentStatus.Scheduled;
    }

    public void Cancel()
    {
        Status = AppointmentStatus.Cancelled;
    }

    public void MarkAsAttended()
    {
        Status = AppointmentStatus.Attended;
    }
}
