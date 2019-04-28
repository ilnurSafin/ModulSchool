using System;
using System.Threading.Tasks;

using MassTransit;


using ModulSchool.Models;
using ModulSchool.Commands;

namespace ModulSchool.BusinessLogic
{
    public class AppendUsersRequestHandler
    {
        private readonly IBus _bus;

        public AppendUsersRequestHandler(IBus bus)
        {
            _bus = bus;
        }

        public async Task<User> Handle(User _user)
        {
            //генерируем пользователю уникальный ид
            Guid guid = Guid.NewGuid();
            _user.Id = guid;

            //отправляем команду на добавление нового пользователя в шину
            await _bus.Send(new AppendUserCommand()
            {
                user = _user //команда также имеет поле user, поэтому параметру был присвоен идентификатор _user 
            });

             //пользователю возвращаем результат, даже если команда все ещё в стеке шины. ПС:ленивый ученик =)
            return await Task.FromResult<User>(_user);
        }
    }
}