using System.Threading.Tasks;
using ModulSchool.Commands;
using MassTransit;
using ModulSchool.Services.Interfaces;
using ModulSchool.Models;

namespace ModulSchool.Consumers
{
    public class AppendUserConsumer : IConsumer<AppendUserCommand>
    {
        private readonly IUserInfoService _userInfoService;

        public AppendUserConsumer(IUserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
        }

        public async Task Consume(ConsumeContext<AppendUserCommand> context)
        {
            //когда очередь дойдет до нас, выполнится добавление нового пользователя
            await _userInfoService.AppendUser(context.Message.user);
        }
    }
}