using KotanTelegramBot.FunctionApp.Models;
using MediatR;

namespace KotanTelegramBot.FunctionApp.Commands
{
    public class DailyNotifyCommand : IRequest
    {
        public FunctionContext FunctionContext { get; set; }

        public DailyNotifyCommand(FunctionContext context)
        {
            FunctionContext = context;
        }
    }
}