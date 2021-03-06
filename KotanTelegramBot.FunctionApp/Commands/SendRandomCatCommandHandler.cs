using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using KotanTelegramBot.FunctionApp.Models.XML;
using MediatR;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KotanTelegramBot.FunctionApp.Commands
{
    public class SendRandomCatCommandHandler : IRequestHandler<SendRandomCatCommand>
    {
        private readonly TelegramBotClient _botClient;
        private readonly HttpClient _httpClient;

        public SendRandomCatCommandHandler(TelegramBotClient botClient, IHttpClientFactory factory)
        {
            _botClient = botClient;
            _httpClient = factory.CreateClient("catsApiClient");
        }

        public async Task<Unit> Handle(SendRandomCatCommand request, CancellationToken cancellationToken)
        {
            HttpResponseMessage msg = await _httpClient.GetAsync("/api/images/get?format=xml", cancellationToken);
            Stream stream = await msg.Content.ReadAsStreamAsync();
            XmlSerializer serializer = new XmlSerializer(typeof(CatResponse));
            CatResponse response = (CatResponse)serializer.Deserialize(stream);
            await _botClient.SendPhotoAsync(request.Message.Chat.Id, new FileToSend(response.Data.Images.Image.Url), cancellationToken: cancellationToken);
            return new Unit();
        }
    }
}