using HospitalSanVicente.Models;
using System;

namespace HospitalSanVicente.Services
{
    public static class EmailService
    {
        // This method simulates sending an email confirmation for an appointment
        // Returns true if email "sent" successfully, false otherwise
        public static bool SendAppointmentConfirmation(Appointment appointment)
        {
            try
            {
                // Here you would integrate with a real email provider (SMTP, SendGrid, etc.)
                // For now, just simulate sending email:
                Console.WriteLine($"Sending confirmation email to {appointment.Patient.Email}...");
                Console.WriteLine($"Appointment Date/Time: {appointment.DateTime}");
                Console.WriteLine($"Doctor: {appointment.Doctor.FirstName} {appointment.Doctor.LastName}");

                // Simulate success
                return true;
            }
            catch (Exception ex)
            {
                // Log error or handle failure
                Console.WriteLine($"Failed to send email: {ex.Message}");
                return false;
            }
        }
    }
}