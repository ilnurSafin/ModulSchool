using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ModulSchool.BusinessLogic;
using ModulSchool.Models;

namespace ModulSchool.Controllers
{
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly GetUsersInfoRequestHandler _getUsersInfoRequestHandler;
        private readonly AppendUsersRequestHandler _appendUsersRequestHandler;

        public UsersController(GetUsersInfoRequestHandler getUsersInfoRequestHandler, AppendUsersRequestHandler appendUsersRequestHandler)
        {
            _getUsersInfoRequestHandler = getUsersInfoRequestHandler;
            _appendUsersRequestHandler = appendUsersRequestHandler;
        }

        [HttpGet("{id}")]
        public Task<User> GetUserInfo(Guid id)
        {
            return _getUsersInfoRequestHandler.Handle(id);
        }
        
        [HttpPost("append")]
        public Task<User> AppendUser([FromBody] User user)
        {
            return  _appendUsersRequestHandler.Handle(user);
        }
    }
}
