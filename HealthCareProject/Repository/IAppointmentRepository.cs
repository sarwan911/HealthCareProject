﻿using HealthCareProject.Models;
namespace HealthCareProject.Repository
{
    public interface IAppointmentRepository
    {
    Task<Appointment> BookAppointmentAsync(int appointmentId, int sessionId, int patientId, string status);
    Task<Appointment> RescheduleAppointmentAsync(int appointmentId, int newSessionId);
    Task<Appointment> CancelAppointmentAsync(int appointmentId);
    Task<Appointment> GetAppointmentByIdAsync(int appointmentId);
    Task<Appointment> GetByIdAsync(int appointmentId);
    Task<List<Appointment>> GetAllAsync();
    Task AddAsync(Appointment appointment);
    Task UpdateAsync(Appointment appointment);
    Task DeleteAsync(int appointmentId);
    Task<List<Appointment>> GetAppointmentsForPatient(int patientId);
    Task<List<Appointment>> GetAppointmentsForDoctor(int doctorId);
    }
}