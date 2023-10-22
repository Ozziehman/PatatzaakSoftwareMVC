using Microsoft.EntityFrameworkCore;
using PatatzaakSoftwareMVC.Data;
using PatatzaakSoftwareMVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PatatzaakSoftwareMVC.DataAccessLayer
{
    public class UserRepository
    {
        private readonly MainDb _context;

        public UserRepository(MainDb context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.users.ToListAsync();
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.users.FindAsync(id);
        }

        /// <summary>
        /// Add a user to the database 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<User> AddUserAsync(User user)
        {
            _context.users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        /// <summary>
        /// Update a user in the database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<User> UpdateUserAsync(User user)
        {
            var userToUpdate = await _context.users.FindAsync(user.Id);
            userToUpdate.Name = user.Name;
            userToUpdate.Email = user.Email;
            userToUpdate.Password = user.Password;
            userToUpdate.IsAdmin = user.IsAdmin;
            userToUpdate.Points = user.Points;


            await _context.SaveChangesAsync();
            return user;
        }

        /// <summary>
        /// Delete a user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.users.FindAsync(id);

            if (user == null)
                return false;

            _context.users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

