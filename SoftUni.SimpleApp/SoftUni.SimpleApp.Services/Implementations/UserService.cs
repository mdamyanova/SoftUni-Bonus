namespace SoftUni.SimpleApp.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Contracts;
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Services.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {
        private readonly SimpleAppDbContext db;

        public UserService(SimpleAppDbContext db)
        {
            this.db = db;
        }

        public bool Exists(string id)
        {
            return this.db.Users.Any(c => c.Id == id);
        }

        public IEnumerable<UserListingServiceModel> All()
            => this.db
                .Users
                .ProjectTo<UserListingServiceModel>()
                .ToList();

        public UserProfileServiceModel Profile(string id)
            => this.db
                .Users
                .Where(u => u.Id == id)
                .ProjectTo<UserProfileServiceModel>()
                .FirstOrDefault();

        public async Task<UserProfileServiceModel> ProfileAsync(string id)
             => await this.db
                    .Users
                    .Where(u => u.Id == id)
                    .ProjectTo<UserProfileServiceModel>()
                    .FirstOrDefaultAsync();

        public async Task<IEnumerable<Journal>> JournalsAsync(string id)
            => await this.db
                .Journals
                .Where(j => j.UserId == id)
                .ToListAsync();

        public void Edit(string username, string name)
        {
            var user = this.db.Users.Find(name);

            if (user == null)
            {
                return;
            }

            user.UserName = username;

            this.db.SaveChanges();
        }

        public bool Remove(string id)
        {
            // remove tasks?

            // first remove journals
            var journals = this.db.Journals.Where(c => c.UserId == id);

            if (journals.Any())
            {
                this.db.RemoveRange(journals);
            }

            var user = this.db.Users.Find(id);

            if (user == null)
            {
                return false;
            }

            this.db.Users.Remove(user);
            this.db.SaveChanges();

            return true;
        }
    }
}