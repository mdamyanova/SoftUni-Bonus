﻿namespace SoftUni.SimpleApp.Services.Contracts
{
    using Data.Models;
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserService
    {
        bool Exists(string id);

        IEnumerable<UserListingServiceModel> All();

        Task<UserProfileServiceModel> ProfileAsync(string username);

        Task<IEnumerable<Journal>> JournalsAsync(string id);

        bool Remove(string id);

        void Edit(string username, string name);
    }
}