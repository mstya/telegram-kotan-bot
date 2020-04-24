using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using KotanTelegramBot.FunctionApp.Commands;
using MediatR;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace KotanTelegramBot.FunctionApp
{
    public class GetCatFunction
    {
        private readonly Dictionary<string, ICatCommandMessage> _commands;
        private readonly TelemetryClient _telemetryClient;
        private readonly IMediator _mediator;

        public GetCatFunction(TelegramBotClient botClient, TelemetryClient telemetryClient, IMediator mediator)
        {
            _telemetryClient = telemetryClient;
            _mediator = mediator;
            _commands = new Dictionary<string, ICatCommandMessage>
            {
                { "/get_cat", new SendRandomCatCommand() },
                { "/get_gif_cat",  new SendRandomCatGifCommand() },
                { "/get_cat@phenix117bot",  new SendRandomCatCommand() },
                { "/get_gif_cat@phenix117bot", new SendRandomCatGifCommand() },
            };
        }
        
        [FunctionName("GetCatFunction")]
        public async Task<IActionResult> RunAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]
            HttpRequest req, ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            _telemetryClient.TrackTrace(new TraceTelemetry("Request json: " + requestBody));
            Update update = JsonConvert.DeserializeObject<Update>(requestBody);
            if (update == null)
            {
                _telemetryClient.TrackTrace(new TraceTelemetry("Body is empty."));
                return new BadRequestResult();
            }

            Message message = update.Message;
            _telemetryClient.TrackTrace(new TraceTelemetry("Message type: " + message?.Type));
            if (message?.Type == MessageType.TextMessage)
            {
                _telemetryClient.TrackTrace(new TraceTelemetry("Message text: " + message.Text));
                if(_commands.TryGetValue(message.Text, out ICatCommandMessage command))
                {
                    command.Message = message;
                    _telemetryClient.TrackTrace(new TraceTelemetry("Command handler: " + command.GetType()));
                    await _mediator.Send(command);
                }
            }

            return new OkResult();
        }
    }
}