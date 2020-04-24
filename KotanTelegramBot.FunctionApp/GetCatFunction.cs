using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using KotanTelegramBot.FunctionApp.Commands;
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
        private readonly Dictionary<string, IKotanCommand> _commands;
        private readonly TelemetryClient _telemetryClient;
        
        public GetCatFunction(TelegramBotClient botClient, TelemetryClient telemetryClient)
        {
            _telemetryClient = telemetryClient;
            _commands = new Dictionary<string, IKotanCommand>
            {
                { "/get_cat", new RandomCatCommand(botClient) },
                { "/get_gif_cat", new RandomCatGifCommand(botClient) },
                { "/get_cat@phenix117bot", new RandomCatCommand(botClient) },
                { "/get_gif_cat@phenix117bot", new RandomCatGifCommand(botClient) },
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
                if(_commands.TryGetValue(message.Text, out IKotanCommand command))
                { 
                    _telemetryClient.TrackTrace(new TraceTelemetry("Command handler: " + command.GetType()));
                    await command.Execute(message);
                }
            }

            return new OkResult();
        }
    }
}