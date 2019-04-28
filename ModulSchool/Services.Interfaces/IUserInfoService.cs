using System;
using System.Threading.Tasks;
using ModulSchool.Models;

namespace ModulSchool.Services.Interfaces
{
    public interface IUserInfoService
    {
        Task<User> GetById(Guid id);
        Task<User> AppendUser(User user);
    }
}