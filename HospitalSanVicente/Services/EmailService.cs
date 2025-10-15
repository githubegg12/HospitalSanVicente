using System.Net;
using System.Net.Mail;
using HospitalSanVicente.Models;
using Microsoft.Extensions.Configuration;

namespace HospitalSanVicente.Services
{
    public static class EmailService
    {
        private static IConfiguration _configuration;

        public static void Configure(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static bool SendAppointmentConfirmation(Appointment appointment)
        {
            try
            {
                var smtpSettings = _configuration.GetSection("SmtpSettings");

                var host = smtpSettings.GetValue<string>("Host");
                var port = smtpSettings.GetValue<int>("Port");
                var username = smtpSettings.GetValue<string>("Username");
                var password = smtpSettings.GetValue<string>("Password");
                var enableSsl = smtpSettings.GetValue<bool>("EnableSsl");
                var fromEmail = smtpSettings.GetValue<string>("FromEmail");

                using var client = new SmtpClient(host, port)
                {
                    Credentials = new NetworkCredential(username, password),
                    EnableSsl = enableSsl,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(fromEmail),
                    Subject = "Appointment Confirmation",
                    Body = $"Dear {appointment.Patient.FirstName}, your appointment with Dr. {appointment.Doctor.LastName} is confirmed for {appointment.DateTime}.",
                    IsBodyHtml = false,
                };

                mailMessage.To.Add(appointment.Patient.Email);

                client.Send(mailMessage);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message);
                return false;
            }
        }

        public static bool SendPatientRegistrationConfirmation(Patient patient)
        {
            try
            {
                var smtpSettings = _configuration.GetSection("SmtpSettings");

                var host = smtpSettings.GetValue<string>("Host");
                var port = smtpSettings.GetValue<int>("Port");
                var username = smtpSettings.GetValue<string>("Username");
                var password = smtpSettings.GetValue<string>("Password");
                var enableSsl = smtpSettings.GetValue<bool>("EnableSsl");
                var fromEmail = smtpSettings.GetValue<string>("FromEmail");

                using var client = new SmtpClient(host, port)
                {
                    Credentials = new NetworkCredential(username, password),
                    EnableSsl = enableSsl,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(fromEmail),
                    Subject = "Welcome to Hospital San Vicente",
                    Body = $"Dear {patient.FirstName}, thank you for registering at Hospital San Vicente.",
                    IsBodyHtml = false,
                };

                mailMessage.To.Add(patient.Email);

                client.Send(mailMessage);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message);
                return false;
            }
        }

        public static bool SendDoctorRegistrationConfirmation(Doctor doctor)
        {
            try
            {
                var smtpSettings = _configuration.GetSection("SmtpSettings");

                var host = smtpSettings.GetValue<string>("Host");
                var port = smtpSettings.GetValue<int>("Port");
                var username = smtpSettings.GetValue<string>("Username");
                var password = smtpSettings.GetValue<string>("Password");
                var enableSsl = smtpSettings.GetValue<bool>("EnableSsl");
                var fromEmail = smtpSettings.GetValue<string>("FromEmail");

                using var client = new SmtpClient(host, port)
                {
                    Credentials = new NetworkCredential(username, password),
                    EnableSsl = enableSsl,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(fromEmail),
                    Subject = "Welcome to Hospital San Vicente",
                    Body = $"Dear Dr. {doctor.LastName}, thank you for registering at Hospital San Vicente.",
                    IsBodyHtml = false,
                };

                mailMessage.To.Add(doctor.Email);

                client.Send(mailMessage);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message);
                return false;
            }
        }
    }
}