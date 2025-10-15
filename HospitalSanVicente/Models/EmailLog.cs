namespace HospitalSanVicente.Models;

public class EmailLog
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid AppointmentId { get; set; }  // Link to appointment
    public string PatientEmail { get; set; } = string.Empty;
    public bool Sent { get; set; }            // true if email was sent, false if failed
    public DateTime Timestamp { get; set; }  // When email was sent
}