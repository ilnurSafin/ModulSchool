using System;
using System.Threading.Tasks;
using ModulSchool.Models;
using ModulSchool.Services.Interfaces;


namespace ModulSchool.BusinessLogic
{
    public class GetUsersInfoRequestHandler
    {
        private readonly IUserInfoService _userInfoService;

        public GetUsersInfoRequestHandler(IUserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
        }

        public Task<User> Handle(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Некорректный идентификатор пользователя", nameof(id));
            }

            return _userInfoService.GetById(id);
        }
    }
}
