using System.Threading.Tasks;
using KotanTelegramBot.FunctionApp.Commands;
using KotanTelegramBot.FunctionApp.Models;
using MediatR;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Table;

namespace KotanTelegramBot.FunctionApp
{
    public class SendDailyCatFunction
    {
        private readonly IMediator _mediator;

        public SendDailyCatFunction(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        // 22:00 every day
        [FunctionName("SendDailyCatFunction")]
        public async Task RunAsync(
            [TimerTrigger("0 0 22 * * *")] TimerInfo myTimer,
            [Table("DailyCatSubscribers")] CloudTable subscribersTable,
            ILogger log)
        {
            await _mediator.Send(new DailyNotifyCommand(new FunctionContext
            {
                SubscribersCloudTable = subscribersTable
            }));
        }
    }
}