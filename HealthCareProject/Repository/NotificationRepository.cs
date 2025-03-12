﻿using HealthCareProject.Models;
using HealthCareProject.Data;
using HealthCareProject.Repository;
using Microsoft.EntityFrameworkCore;

namespace HealthCareProject.Repository
{
    public class NotificationRepository : INotificationRepository
    {
            private readonly Context _context;
            public NotificationRepository(Context context) : base()
            {
                _context = context;
            }

        public async Task<Notification> SendNotificationAsync(int userId, string type, string message, string status)
        {
            return await _context.Notifications
                .FromSqlRaw("EXEC sp_ManageNotification @p0, @p1, @p2, @p3",
                    userId, type, message, status)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        // 📌 2️⃣ Get All Notifications for a User
        public async Task<List<Notification>> GetUserNotificationsAsync(int userId)
        {
            return await _context.Notifications
                .FromSqlRaw("EXEC sp_GetNotificationsByUserId @p0",
                    userId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddNotificationAsync(int userId, string message)
        {
            var notification = new Notification
            {
                UserId = userId,
                Message = message
            };
            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Notification>> GetNotificationsByUserIdAsync(int userId)
        {
            return await _context.Notifications.Where(n => n.UserId == userId).ToListAsync();
        }
    }
}