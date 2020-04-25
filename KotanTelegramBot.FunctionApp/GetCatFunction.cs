using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using KotanTelegramBot.FunctionApp.Commands;
using KotanTelegramBot.FunctionApp.Models;
using MediatR;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace KotanTelegramBot.FunctionApp
{
    public class GetCatFunction
    {
        private readonly Dictionary<string, Func<Message, FunctionContext, Task>> _commands;
        private readonly TelemetryClient _telemetryClient;

        public GetCatFunction(TelemetryClient telemetryClient, IMediator mediator)
        {
            _telemetryClient = telemetryClient;
            _commands = new Dictionary<string, Func<Message, FunctionContext, Task>>
            {
                { "/get_cat", (message, context) => mediator.Send(new SendRandomCatCommand { Message = message }) },
                { "/get_gif_cat",  (message, context) => mediator.Send(new SendRandomCatGifCommand { Message = message }) },
                { "/get_cat@phenix117bot",  (message, context) => mediator.Send(new SendRandomCatCommand { Message = message }) },
                { "/get_gif_cat@phenix117bot", (message, context) => mediator.Send(new SendRandomCatGifCommand { Message = message }) },
                { "/daily_subscribe", (message, context) => mediator.Send(new DailySubscribeCommand { Message = message, FunctionContext = context }) },
                { "/daily_unsubscribe", (message, context) => mediator.Send(new DailyUnsubscribeCommand { Message = message, FunctionContext = context })},
            };
        }
        
        [FunctionName("GetCatFunction")]
        public async Task<IActionResult> RunAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            [Table("DailyCatSubscribers")] CloudTable subscribersTable,
            ILogger log)
        {
            var functionContext = new FunctionContext
            {
                SubscribersCloudTable = subscribersTable
            };
            
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            _telemetryClient.TrackTrace(new TraceTelemetry("Request json: " + requestBody));
            Update update = JsonConvert.DeserializeObject<Update>(requestBody);
            if (update == null)
            {
                _telemetryClient.TrackTrace(new TraceTelemetry("Body is empty."));
                return new BadRequestResult();
            }

            await HandleMessage(update, functionContext);

            return new OkResult();
        }

        private async Task HandleMessage(Update update, FunctionContext context)
        {
            Message message = update.Message;
            _telemetryClient.TrackTrace(new TraceTelemetry("Message type: " + message?.Type));
            if (message?.Type == MessageType.TextMessage)
            {
                _telemetryClient.TrackTrace(new TraceTelemetry("Message text: " + message.Text));
                if (_commands.TryGetValue(message.Text, out Func<Message, FunctionContext, Task> command))
                {
                    _telemetryClient.TrackTrace(new TraceTelemetry("Command handler: " + command.GetType()));
                    await command(message, context);
                }
            }
        }
    }
}