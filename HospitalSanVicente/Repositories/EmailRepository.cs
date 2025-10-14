using HospitalSanVicente.Data;
using HospitalSanVicente.Models;
using System.Collections.Generic;

namespace HospitalSanVicente.Repositories;

public class EmailLogRepository
{
    private readonly Database _database;

    public EmailLogRepository(Database database)
    {
        _database = database;
    }

    // Add a new email log entry
    public void Add(EmailLog emailLog)
    {
        _database.EmailLogs.Add(emailLog);
    }

    // Get all email logs
    public List<EmailLog> GetAll()
    {
        return _database.EmailLogs;
    }

    // Optionally: Get logs by AppointmentId
    public List<EmailLog> GetByAppointmentId(Guid appointmentId)
    {
        return _database.EmailLogs.Where(e => e.AppointmentId == appointmentId).ToList();
    }
}