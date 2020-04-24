using System;
using System.Linq;
using System.Threading.Tasks;
using KotanTelegramBot.FunctionApp.Commands;
using KotanTelegramBot.FunctionApp.Models;
using MediatR;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;

namespace KotanTelegramBot.FunctionApp
{
    public class SendDailyCatFunction
    {
        private readonly IMediator _mediator;

        public SendDailyCatFunction(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        // 14:00 every day
        [FunctionName("SendDailyCatFunction")]
        public void RunAsync(
            [TimerTrigger("0 0 14 * * *")] TimerInfo myTimer,
            [Table("DailyCatSubscribers", "subscribers")] IQueryable<CatSubscriber> subscribers,
            ILogger log)
        {
            var subscribersList = subscribers.ToList();
            
            subscribersList.ForEach(x =>
            {
                _mediator.Send(new SendRandomCatGifCommand
                {
                    Message = new Message
                    {
                        Chat = new Chat
                        {
                            Id = x.ChannelId
                        }
                    }
                });
            });
        }
    }
}