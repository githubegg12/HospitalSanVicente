using System;
using System.Collections.Generic;
using ClinicaSalud.Services;
using HospitalSanVicente.Models;
using HospitalSanVicente.Repositories;
using HospitalSanVicente.Data;
using HospitalSanVicente.Utils; // Para InputValidator o clases similares

namespace HospitalSanVicente.Services
{
    public class AppointmentService
    {
        private readonly AppointmentRepository _appointmentRepository;
        private readonly EmailLogRepository _emailLogRepository; // For logging email status
        private readonly PatientRepository _patientRepository;
        private readonly DoctorRepository _doctorRepository;

        // Constructor with dependencies injected
        public AppointmentService(Database database)
        {
            _appointmentRepository = new AppointmentRepository(database);
            _emailLogRepository = new EmailLogRepository(database);
            _patientRepository = new PatientRepository(database);
            _doctorRepository = new DoctorRepository(database);
        }

        // Schedule a new appointment with validations
        public Appointment? ScheduleAppointment(Patient patient, Doctor doctor, DateTime dateTime)
        {
            if (patient == null || doctor == null)
            {
                Console.WriteLine("Invalid patient or doctor.");
                return null;
            }

            if (dateTime <= DateTime.Now)
            {
                Console.WriteLine("Appointment date/time must be in the future.");
                return null;
            }

            if (_appointmentRepository.HasConflict(patient.Id, doctor.Id, dateTime))
            {
                Console.WriteLine("Scheduling conflict: patient or doctor already has an appointment at that time.");
                return null;
            }

            var appointment = new Appointment(patient, doctor, dateTime);
            _appointmentRepository.Add(appointment);

            bool emailSent = EmailService.SendAppointmentConfirmation(appointment);

            var emailLog = new EmailLog
            {
                Id = Guid.NewGuid(),
                AppointmentId = appointment.Id,
                PatientEmail = patient.Email,
                Sent = emailSent,
                Timestamp = DateTime.Now
            };
            _emailLogRepository.Add(emailLog);

            Console.WriteLine(emailSent ? "Confirmation email sent successfully." : "Failed to send confirmation email.");

            return appointment;
        }

        // Cancel appointment by ID
        public bool CancelAppointment(Guid appointmentId)
        {
            var appointment = _appointmentRepository.GetById(appointmentId);
            if (appointment == null)
            {
                Console.WriteLine("Appointment not found.");
                return false;
            }
            if (appointment.Status == AppointmentStatus.Cancelled)
            {
                Console.WriteLine("Appointment is already cancelled.");
                return false;
            }

            appointment.Cancel();
            _appointmentRepository.Update(appointment);
            Console.WriteLine("Appointment cancelled successfully.");
            return true;
        }

        // Mark appointment as attended by ID
        public bool MarkAsAttended(Guid appointmentId)
        {
            var appointment = _appointmentRepository.GetById(appointmentId);
            if (appointment == null)
            {
                Console.WriteLine("Appointment not found.");
                return false;
            }
            if (appointment.Status != AppointmentStatus.Scheduled)
            {
                Console.WriteLine("Only scheduled appointments can be marked as attended.");
                return false;
            }

            appointment.MarkAsAttended();
            _appointmentRepository.Update(appointment);
            Console.WriteLine("Appointment marked as attended.");
            return true;
        }

        // List appointments by patient
        public List<Appointment> ListByPatient(Guid patientId)
        {
            return _appointmentRepository.GetByPatient(patientId);
        }

        // List appointments by doctor
        public List<Appointment> ListByDoctor(Guid doctorId)
        {
            return _appointmentRepository.GetByDoctor(doctorId);
        }

        // --- METHODS TO INTERACT WITH USER (CONSOLE) ---

        // Schedule appointment by reading input from console
        public void ScheduleAppointmentFromConsole()
        {
            try
            {
                Console.WriteLine("--- Schedule Appointment ---");

                // Read Patient by DocumentID
                int patientDocId = InputValidator.ReadNonNegativeInt("Enter Patient Document ID");
                var patient = _patientRepository.GetByDocumentId(patientDocId);
                if (patient == null)
                {
                    Console.WriteLine("Patient not found.");
                    return;
                }

                // Read Doctor by DocumentID
                int doctorDocId = InputValidator.ReadNonNegativeInt("Enter Doctor Document ID");
                var doctor = _doctorRepository.GetByDocumentId(doctorDocId);
                if (doctor == null)
                {
                    Console.WriteLine("Doctor not found.");
                    return;
                }

                // Read DateTime for appointment
                DateTime appointmentDateTime = InputValidator.ReadDateTime("Enter appointment date and time (yyyy-MM-dd HH:mm)");

                var appointment = ScheduleAppointment(patient, doctor, appointmentDateTime);

                if (appointment != null)
                    Console.WriteLine($"Appointment scheduled successfully for {appointmentDateTime}.");
                else
                    Console.WriteLine("Failed to schedule appointment.");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Appointment scheduling canceled by user.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error scheduling appointment: {ex.Message}");
            }
        }

        // List appointments by patient from console input
        public void ListAppointmentsByPatientFromConsole()
        {
            try
            {
                Console.WriteLine("--- List Appointments By Patient ---");
                int patientDocId = InputValidator.ReadNonNegativeInt("Enter Patient Document ID");
                var patient = _patientRepository.GetByDocumentId(patientDocId);
                if (patient == null)
                {
                    Console.WriteLine("Patient not found.");
                    return;
                }

                var appointments = ListByPatient(patient.Id);

                if (appointments.Count == 0)
                {
                    Console.WriteLine("No appointments found for this patient.");
                    return;
                }

                foreach (var app in appointments)
                {
                    Console.WriteLine($"Appointment ID: {app.Id} | Doctor: {app.Doctor.FirstName} {app.Doctor.LastName} | Date: {app.DateTime} | Status: {app.Status}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error listing appointments: {ex.Message}");
            }
        }

        // List appointments by doctor from console input
        public void ListAppointmentsByDoctorFromConsole()
        {
            try
            {
                Console.WriteLine("--- List Appointments By Doctor ---");
                int doctorDocId = InputValidator.ReadNonNegativeInt("Enter Doctor Document ID");
                var doctor = _doctorRepository.GetByDocumentId(doctorDocId);
                if (doctor == null)
                {
                    Console.WriteLine("Doctor not found.");
                    return;
                }

                var appointments = ListByDoctor(doctor.Id);

                if (appointments.Count == 0)
                {
                    Console.WriteLine("No appointments found for this doctor.");
                    return;
                }

                foreach (var app in appointments)
                {
                    Console.WriteLine($"Appointment ID: {app.Id} | Patient: {app.Patient.FirstName} {app.Patient.LastName} | Date: {app.DateTime} | Status: {app.Status}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error listing appointments: {ex.Message}");
            }
        }

        // Cancel appointment from console input
        public void CancelAppointmentFromConsole()
        {
            try
            {
                Console.WriteLine("--- Cancel Appointment ---");
                Guid appointmentId = InputValidator.ReadGuid("Enter Appointment ID to cancel");
                if (CancelAppointment(appointmentId))
                    Console.WriteLine("Appointment cancelled.");
                else
                    Console.WriteLine("Failed to cancel appointment.");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Appointment cancellation canceled by user.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error cancelling appointment: {ex.Message}");
            }
        }

        // Mark appointment as attended from console input
        public void MarkAppointmentAttendedFromConsole()
        {
            try
            {
                Console.WriteLine("--- Mark Appointment as Attended ---");
                Guid appointmentId = InputValidator.ReadGuid("Enter Appointment ID to mark as attended");
                if (MarkAsAttended(appointmentId))
                    Console.WriteLine("Appointment marked as attended.");
                else
                    Console.WriteLine("Failed to mark appointment as attended.");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Operation canceled by user.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error marking appointment as attended: {ex.Message}");
            }
        }
        
    }
}
