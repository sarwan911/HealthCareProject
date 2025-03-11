using HealthCareProject.Data;
using HealthCareProject.Models;
using Microsoft.EntityFrameworkCore;
using HealthCareProject.Repository;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System;

namespace HealthCareProject.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;
        // 🔹 Register a new user
        public async Task<int> RegisterUserAsync(User user)
        {
            var userIdParam = new SqlParameter("@UserId", SqlDbType.Int) { Direction = ParameterDirection.Output };
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_RegisterUser @p0, @p1, @p2, @p3, @p4, @p5, @p6, @UserId OUTPUT",
                user.Name, user.Role, user.Email, user.Age, user.Phone, user.Address, user.Password, userIdParam);
            return (int)userIdParam.Value;
        }

        // 🔹 Find user by email (for authentication)
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .FromSqlRaw("EXEC sp_GetUserByEmail @p0", email)
                .FirstOrDefaultAsync();
        }

        // 🔹 Find user by ID
        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _context.Users
                .FromSqlRaw("EXEC sp_GetUserById @p0", userId)
                .FirstOrDefaultAsync();
        }

        public async Task<User> GetByIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                await _context.SaveChangesAsync();
            }
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}